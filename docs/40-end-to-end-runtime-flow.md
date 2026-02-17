# End-to-End Runtime Flow

## 1. Назначение
Документ описывает сквозной runtime-поток приложения `Review` в состоянии `as-is`: от запуска процесса до сетевого обмена со станциями, обработки сигналов, записи данных в MySQL и печати этикетки.

Документ привязан к коду и используется как source-of-truth для:
- операционного runbook (`docs/50-operations-runbook.md`);
- troubleshooting (`docs/60-error-catalog-and-troubleshooting.md`).

## 2. Ключевые runtime-компоненты
| Компонент | Роль в потоке | Кодовые точки |
|---|---|---|
| `Program` | Bootstrap процесса и переход `Login -> Main` | `Program.cs:15`, `Program.cs:19`, `Program.cs:23` |
| `FrmLogIn` | Аутентификация пользователя | `FrmLogIn.cs:31`, `FrmLogIn.cs:35`, `DBHelper.cs:349` |
| `FrmMain` | Оркестрация UI и запуска линии | `FrmMain.cs:90`, `FrmMain.cs:214`, `FrmMain.cs:507` |
| `ProductLine` | TCP listener, accept/receive/send со станциями | `ProductLine.cs:122`, `ProductLine.cs:147`, `ProductLine.cs:215`, `ProductLine.cs:253` |
| `MyTimer` | 200ms business loop, обработка статусов/feedback, запись/печать | `MyTimer.cs:69`, `MyTimer.cs:92`, `MyTimer.cs:98`, `MyTimer.cs:492`, `MyTimer.cs:667` |
| `DBHelper` | Доступ к MySQL (login, station data, final tables, RFID) | `DBHelper.cs:18`, `DBHelper.cs:629`, `DBHelper.cs:771` |
| `MyConvert` | Декодирование входных пакетов и кодирование ответа PLC | `MyConvert.cs:11`, `MyConvert.cs:48` |
| `log4net` | Операционные/ошибочные/NG-логи | `Properties/AssemblyInfo.cs:17`, `config/log4net.config:75`, `config/log4net.config:87`, `config/log4net.config:98` |

## 3. Сквозной поток исполнения (happy path)

### 3.1 Bootstrap и login
1. Процесс стартует из `Main`, включает WinForms окружение и открывает модальный логин.
Код: `Program.cs:15`, `Program.cs:17`, `Program.cs:20`.
2. По нажатию `Login` форма читает `txtUser/txtPwd` и вызывает `DBHelper.UserlogIn(...)`.
Код: `FrmLogIn.cs:31`, `FrmLogIn.cs:33`, `FrmLogIn.cs:35`, `DBHelper.cs:352`.
3. При `LoginFlag == true` закрывается окно логина и запускается `FrmMain(user, level)`.
Код: `FrmLogIn.cs:36`, `FrmLogIn.cs:39`, `Program.cs:21`, `Program.cs:23`.

### 3.2 Инициализация `FrmMain`
1. Конструктор `FrmMain` вызывает `ReadIPAndPort()`: читает таблицу `station`, извлекает серверный endpoint и карту станций.
Код: `FrmMain.cs:93`, `FrmMain.cs:214`, `FrmMain.cs:220`, `FrmMain.cs:227`.
2. Параллельно инициализируются локализация и UI-строки.
Код: `FrmMain.cs:98`, `FrmMain.cs:104`, `FrmMain.cs:137`.
3. В `FrmMain_Load` заполняются продукты и градации.
Код: `FrmMain.cs:565`, `FrmMain.cs:567`, `FrmMain.cs:568`.

### 3.3 Подготовка перед стартом линии (в UI)
1. Оператор выбирает `Product` и `Grade`.
Код: `FrmMain.cs:522`, `FrmMain.cs:690`, `FrmMain.cs:695`.
2. Выбирается шаблон печати `.prn` через `button1` (`OpenFileDialog`), путь сохраняется в `proCodeTemplate`.
Код: `FrmMain.cs:769`, `FrmMain.cs:787`, `FrmMain.cs:794`.
3. При включенной сверке выполняется `CodeVerification(...)` по `standardcode`/`scancode`.
Код: `FrmMain.cs:527`, `FrmMain.cs:529`, `FrmMain.cs:890`, `FrmMain.cs:893`.

### 3.4 Нажатие Start и запуск runtime
1. При старте обновляется текущий печатный код (`printcode`) через `GenerateNextCode`.
Код: `FrmMain.cs:515`, `FrmMain.cs:516`, `Utils/Utils.cs:33`.
2. Валидируются обязательные поля: шаблон, продукт, грейд.
Код: `FrmMain.cs:518`, `FrmMain.cs:522`.
3. Проверяется, что серверный TCP-порт свободен.
Код: `FrmMain.cs:545`, `MyTools.cs:81`.
4. Читается `productType` из таблицы `product`.
Код: `FrmMain.cs:580`, `FrmMain.cs:584`, `FrmMain.cs:587`.
5. Выполняется `TechnologyDataConfig()`:
- выбираются станции процесса (`selectStationList`);
- создается `ProductLine` (TCP listener + runtime-словарь станций);
- выставляются флаги heartbeat/run/enable.
Код: `FrmMain.cs:360`, `FrmMain.cs:293`, `DBHelper.cs:807`, `FrmMain.cs:364`, `FrmMain.cs:309`.
6. Создается `MyTimer` и запускается тиковый цикл 200ms.
Код: `FrmMain.cs:552`, `FrmMain.cs:553`, `MyTimer.cs:69`, `MyTimer.cs:84`.
7. UI-блок настройки выключается, повторный `Start` блокируется.
Код: `FrmMain.cs:562`, `FrmMain.cs:563`.

## 4. Сетевой runtime-контур (TCP)

### 4.1 Listener и прием подключений
1. `ProductLine.startLisen()` создает server socket, bind/listen и уходит в `AcceptSocketClient` через `ThreadPool`.
Код: `ProductLine.cs:122`, `ProductLine.cs:125`, `ProductLine.cs:129`, `ProductLine.cs:133`.
2. При `Accept` входящий endpoint:
- отбрасывается при дублирующем подключении;
- отбрасывается как нелегитимный, если endpoint отсутствует в конфигурации станций;
- маппится в used/non-used словари при валидном источнике.
Код: `ProductLine.cs:160`, `ProductLine.cs:171`, `ProductLine.cs:180`, `ProductLine.cs:192`, `ProductLine.cs:199`.
3. Для валидного клиента создается отдельная receive-задача.
Код: `ProductLine.cs:206`.

### 4.2 Receive/Send цикл
1. `ReceiveData` читает пакет, декодирует бинарный протокол в `StationData`.
Код: `ProductLine.cs:225`, `ProductLine.cs:226`, `ProductLine.cs:227`, `MyConvert.cs:11`.
2. После обработки входа немедленно отправляется `MasterData` ответ.
Код: `ProductLine.cs:250`, `ProductLine.cs:253`, `ProductLine.cs:259`, `MyConvert.cs:48`.
3. На исключении канал закрывается, станция помечается disconnected.
Код: `ProductLine.cs:229`, `ProductLine.cs:238`, `ProductLine.cs:244`, `ProductLine.cs:245`.

## 5. Business loop `MyTimer` (каждые 200 ms)

### 5.1 Оркестрация тика
Каждый тик вызывает два потока логики:
- `DealNetUsedData(...)` для станций, входящих в текущий техпроцесс;
- `DealNetDownLineUsedData(...)` для остальных станций.

Код: `MyTimer.cs:92`, `MyTimer.cs:94`, `MyTimer.cs:95`.

### 5.2 Used stations: семантика статусов
| Сигнал | Действие | Кодовые точки |
|---|---|---|
| `status == 1` | Проверка допуска изделия/маршрута, выставление `feedBack` (`1/41/91/92/93/94`) | `MyTimer.cs:118`, `MyTimer.cs:124`, `MyTimer.cs:134`, `MyTimer.cs:183` |
| `status == 2` | Сохранение station data; обновление `stationprocode`; на `OP40` запись `finalprintcode` и печать | `MyTimer.cs:198`, `MyTimer.cs:213`, `MyTimer.cs:217`, `MyTimer.cs:224`, `MyTimer.cs:236` |
| `status == 3` | Привязка RFID к коду изделия | `MyTimer.cs:242`, `MyTimer.cs:258`, `DBHelper.cs:771` |
| `status == 4` | Отвязка RFID и возврат кода | `MyTimer.cs:265`, `MyTimer.cs:271`, `MyTimer.cs:284`, `DBHelper.cs:785` |
| `status == 21/31` | Маршрутизация двухпоточных станций (`leftOrRight`) + контроль порядка | `MyTimer.cs:292`, `MyTimer.cs:310`, `MyTimer.cs:327`, `MyTimer.cs:354` |
| `status == 22/24/32/34` | Сохранение данных split-станций и обновление общего результата | `MyTimer.cs:368`, `MyTimer.cs:385`, `MyTimer.cs:397`, `MyTimer.cs:400` |
| `status == 41` | Финальная проверка печатного кода: update `printcode` + `scanCode` | `MyTimer.cs:410`, `MyTimer.cs:413`, `MyTimer.cs:417`, `MyTimer.cs:418` |
| `status == 0` | Сброс флагов исполнения/сохранения и `feedBack` | `MyTimer.cs:431`, `MyTimer.cs:438`, `MyTimer.cs:442` |
| `by2 == 88/89` | Внештатное сохранение без штатной трассировки | `MyTimer.cs:447`, `MyTimer.cs:460` |
| `by2 == 42` | One-shot печать по отдельному сигналу (`setPrint`) | `MyTimer.cs:472`, `MyTimer.cs:475`, `MyTimer.cs:479` |

### 5.3 Non-used stations: семантика статусов
| Сигнал | Действие | Кодовые точки |
|---|---|---|
| `status == 1` | Проверка одиночной станции + сохранение/блокировка дубля | `MyTimer.cs:511`, `MyTimer.cs:549`, `MyTimer.cs:557`, `MyTimer.cs:562` |
| `status == 2` | Обновление записи станции | `MyTimer.cs:565`, `DBHelper.cs:711` |
| `status == 3/4` | RFID bind/unbind | `MyTimer.cs:572`, `MyTimer.cs:595`, `DBHelper.cs:771`, `DBHelper.cs:795` |
| `status == 82` | Сохранение в `OP80_2` | `MyTimer.cs:619`, `DBHelper.cs:819` |
| `by2 == 88/89` | Внештатное сохранение (аналог used-ветки) | `MyTimer.cs:628`, `MyTimer.cs:641` |
| `status == 0` | Сброс `haveSave` и `feedBack` | `MyTimer.cs:658`, `MyTimer.cs:660`, `MyTimer.cs:661` |

Примечание по OP90: логика `Mode/StartIndex/CodeLength/Delimiter` есть в `MyTimer`, но runtime-инициализация `dicConfig` в `FrmMain` закомментирована.
Код: `MyTimer.cs:515`, `MyTimer.cs:518`, `FrmMain.cs:561`.

## 6. Потоки записи в БД
1. Базовое подключение берется из `connectMySql` в `App.config`.
Код: `DBHelper.cs:18`, `App.config:9`.
2. Сырые station-данные пишутся через `SaveStationData`/`updateStationData*`.
Код: `DBHelper.cs:629`, `DBHelper.cs:711`, `DBHelper.cs:755`, `DBHelper.cs:762`.
3. Итог по изделию обновляется в `stationprocode`.
Код: `DBHelper.cs:554`, `DBHelper.cs:511`.
4. Печатные данные и связки кодов пишутся в `finalprintcode`.
Код: `DBHelper.cs:517`, `DBHelper.cs:550`.
5. Текущий печатный счетчик хранится в `printcode`.
Код: `DBHelper.cs:919`, `DBHelper.cs:929`.
6. RFID-связки ведутся в `rfidbind`.
Код: `DBHelper.cs:771`, `DBHelper.cs:785`, `DBHelper.cs:795`.

## 7. Контур печати
1. Источник шаблона: выбранный оператором `.prn` (`proCodeTemplateFileName`).
Код: `FrmMain.cs:794`, `MyTimer.cs:669`.
2. Шаблон модифицируется и записывается во временный файл `D:\Marking Lable Template\11.prn`.
Код: `MyTimer.cs:670`, `MyTimer.cs:682`, `MyTimer.cs:693`.
3. Отправка в Zebra выполняется по TCP:9100 на hardcoded IP `192.168.1.45`.
Код: `MyTimer.cs:668`, `MyTimer.cs:703`, `MyTimer.cs:706`.

## 8. Наблюдаемость
1. Логгеры:
- `OperationLogger` -> `logs/operation/YYYYMMDD/YYYYMMDD_HH.log`;
- `ErrorLogger` -> `logs/error/YYYYMMDD/YYYYMMDD_HH.log`;
- `NGPROLogger` -> `logs/NG_info/YYYYMMDD/YYYYMMDD_HH.log`.
Код: `Utils/LogFile.cs:11`, `Utils/LogFile.cs:12`, `Utils/LogFile.cs:13`, `config/log4net.config:75`, `config/log4net.config:87`, `config/log4net.config:98`.
2. Сетевая диагностика доступна в `FrmNet` (connected/status/feedBack/коды).
Код: `FrmMain.cs:609`, `FrmMain.cs:614`, `FrmNet.cs:95`, `FrmNet.cs:127`.
3. UI heartbeat раз в 1 секунду обновляет статус и heartbeat-флаги.
Код: `FrmMain.Designer.cs:480`, `FrmMain.cs:597`, `FrmMain.cs:603`.

## 9. Остановка runtime и ограничения `as-is`
1. Реализованный `MyTimer.Stop()` существует, но из UI не вызывается.
Код: `MyTimer.cs:87`, `FrmMain.cs:501`.
2. Текущий `btnStop_Click` выполняет действия только если `dealNetData == null`; при запущенной линии показывает сообщение и не останавливает loop.
Код: `FrmMain.cs:497`, `FrmMain.cs:504`.
3. Закрытие формы только сбрасывает `MessageBoxHelper.IsWorking`; явного shutdown TCP listener/таймера нет.
Код: `FrmMain.cs:571`, `FrmMain.cs:574`, `MessageBoxHelper.cs:68`.

Практический вывод: штатная остановка в текущей версии равна завершению процесса приложения. Детальный регламент см. `docs/50-operations-runbook.md`.



