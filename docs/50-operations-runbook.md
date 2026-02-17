# Operations Runbook

## 1. Назначение
Документ описывает эксплуатационные процедуры для `Review`:
- `preflight` перед запуском;
- `start` линии;
- `stop` линии с учетом текущих ограничений реализации;
- `incident recovery` (быстрое восстановление после типовых сбоев).

Базовый технический контур: `WinForms (.NET 4.8) -> TCP станции -> MySQL -> Zebra`.
Кодовые точки: `LADA.csproj:11`, `ProductLine.cs:122`, `DBHelper.cs:18`, `MyTimer.cs:667`.

## 2. Быстрый operational summary
| Фаза | Цель | Критерий успеха |
|---|---|---|
| Preflight | Убедиться, что инфраструктура и конфиг готовы | Доступны DB/TCP/шаблон печати/логи |
| Start | Корректно запустить runtime-loop | `btnStart` блокируется, станции обновляются, есть новые логи |
| Stop | Безопасно завершить текущий цикл | Процесс `Review.exe` остановлен, порт освобожден |
| Incident recovery | Сократить MTTR | Причина локализована, контур восстановлен, smoke пройден |

## 3. Preflight

### 3.1 Host preflight (разово на хост)
1. Проверить целевую платформу: `.NET Framework 4.8`.
Код: `LADA.csproj:11`.
2. Проверить, что `log4net` подключен assembly-атрибутом и файл лог-конфига копируется в output.
Код: `Properties/AssemblyInfo.cs:17`, `LADA.csproj:414`, `LADA.csproj:415`.
3. Проверить корректность DB-профиля и connection strings:
- `DbProfile` должен быть `prod` или `test`;
- в `connectionStrings` должны существовать `connectMySql_prod` и `connectMySql_test`.
Код: `App.config:9`, `App.config:10`, `App.config:13`, `Utils/DbProfileResolver.cs:36`.
Примечание: plaintext credentials в `connectMySql_prod/connectMySql_test` разрешены как policy-исключение (бессрочно, без compensating controls), см. `AGENTS.md` и `docs/70-security-baseline.md`.
4. Проверить права на запись в каталоги логов (`logs/operation`, `logs/error`, `logs/NG_info`).
Код: `config/log4net.config:75`, `config/log4net.config:87`, `config/log4net.config:98`.

### 3.2 Shift preflight (перед каждой сменой/стартом)
1. Проверить доступ к MySQL.
Пример:
```powershell
Test-NetConnection 127.0.0.1 -Port 3306
```
2. Проверить активный профиль:
- `DbProfile=prod` для production-запуска;
- `DbProfile=test` только для тестового контура.
Код: `App.config:13`, `Utils/DbProfileResolver.cs:44`.
3. Проверить, что для выбранного профиля существует непустой ключ подключения:
- `prod -> connectMySql_prod`;
- `test -> connectMySql_test`.
Код: `Utils/DbProfileResolver.cs:46`, `Utils/DbProfileResolver.cs:59`.
4. Проверить, что в таблице `station` корректно задана строка server endpoint и станции.
Код чтения: `FrmMain.cs:214`, `FrmMain.cs:220`, `FrmMain.cs:227`.
5. Проверить, что серверный TCP-порт свободен (тот, что в `station` для server).
Код проверки: `FrmMain.cs:545`, `MyTools.cs:81`.
Пример:
```powershell
Get-NetTCPConnection -LocalPort <SERVER_PORT> -State Listen
```
6. Проверить готовность справочников для выбранной модели:
- `product` (для `ProductID`);
- `printcode` (текущий печатный код);
- `standardcode`/`scancode` (если включена верификация партии).
Код: `FrmMain.cs:584`, `DBHelper.cs:919`, `FrmMain.cs:527`, `FrmMain.cs:893`.
7. Проверить файл шаблона печати `.prn` (оператор выбирает в UI перед стартом).
Код: `FrmMain.cs:518`, `FrmMain.cs:781`, `FrmMain.cs:794`.
8. Проверить доступность принтера Zebra по сети.
Код: `MyTimer.cs:668`, `MyTimer.cs:703`.
Пример:
```powershell
Test-NetConnection 192.168.1.45 -Port 9100
```

### 3.3 Preflight gate (разрешение на старт)
Старт разрешен только если одновременно:
1. DB доступна и логин проходит (`FrmLogIn`).
Код: `FrmLogIn.cs:35`, `DBHelper.cs:349`.
2. TCP порт server свободен.
Код: `FrmMain.cs:545`.
3. Выбраны `Product`, `Grade` и `.prn` шаблон.
Код: `FrmMain.cs:518`, `FrmMain.cs:522`.
4. При включенной сверке `CodeVerification()` возвращает `true`.
Код: `FrmMain.cs:529`, `FrmMain.cs:890`.

### 3.4 Переключение профиля БД (prod/test)
1. Остановить линию и завершить `Review.exe` (переключение в runtime не поддерживается).
2. Изменить `DbProfile` в `App.config` на `prod` или `test`.
3. Убедиться, что соответствующий ключ `connectMySql_<profile>` задан корректно.
4. Перезапустить приложение и убедиться, что startup preflight прошел без fail-fast.
Код: `Program.cs:22`, `Program.cs:35`, `Utils/DbProfileResolver.cs:31`.

## 4. Start procedure
1. Запустить `Review.exe`.
Точка входа: `Program.cs:15`.
2. Авторизоваться в `FrmLogIn`.
Код: `FrmLogIn.cs:31`, `FrmLogIn.cs:35`.
3. В `FrmMain` выбрать продукт и грейд, затем выбрать `.prn` файл.
Код: `FrmMain.cs:522`, `FrmMain.cs:769`.
4. При необходимости настроить флаги сверки партии (`EnableVerification`, `alwaysVerification`).
Код: `FrmMain.cs:527`, `FrmMain.cs:673`, `FrmMain.cs:678`.
5. Нажать `Start`.
Код: `FrmMain.cs:507`.
6. Проверить ожидаемые эффекты после старта:
- создан `ProductLine` listener;
- запущен `MyTimer` (200 ms);
- панель настройки и кнопка старта заблокированы.
Код: `FrmMain.cs:364`, `FrmMain.cs:552`, `MyTimer.cs:84`, `FrmMain.cs:562`, `FrmMain.cs:563`.
7. Провести быстрый smoke после запуска:
- открыть окно сети и проверить `connected/status/feedBack`;
- убедиться, что появляются записи в operational-логах;
- убедиться, что по изделиям идут записи в station tables и `stationprocode`.
Код: `FrmMain.cs:609`, `FrmNet.cs:105`, `config/log4net.config:75`, `DBHelper.cs:629`, `DBHelper.cs:511`.

## 5. Stop procedure (as-is)

### 5.1 Плановая остановка
Текущее ограничение реализации: штатный `Stop` таймера не привязан к рабочему сценарию UI.
Код: `MyTimer.cs:87`, `FrmMain.cs:501`.

Порядок действий:
1. Дождаться «тихой точки» (по возможности `status == 0` и `by2 == 0` на активных станциях).
Код: `MyTimer.cs:431`, `MyTimer.cs:484`, `MyTimer.cs:658`.
2. Выполнить завершение приложения (закрыть главное окно / выйти из процесса).
Код: `Program.cs:23`, `FrmMain.cs:571`.
3. Убедиться, что TCP порт server освобожден.
Пример:
```powershell
Get-NetTCPConnection -LocalPort <SERVER_PORT> -ErrorAction SilentlyContinue
```
4. Перед следующим стартом проверить состояние `scancode.checkDigit` (в UI-ветке `btnStop` заложен reset до `-1`, но при рабочем runtime эта ветка не выполняется).
Код: `FrmMain.cs:497`, `FrmMain.cs:502`.

### 5.2 Аварийная остановка
1. Если runtime завис/циклично ошибается, принудительно завершить процесс `Review.exe`.
2. Проверить, что порт освобожден и DB доступна.
3. Перезапустить приложение по стандартной процедуре `Preflight -> Start`.

## 6. Incident recovery

### 6.1 Унифицированный triage (первые 5-10 минут)
1. Зафиксировать `время`, `модель`, `последний productCode`, станцию/контур.
2. Проверить свежие логи:
- `logs/operation/...`;
- `logs/error/...`;
- `logs/NG_info/...`.
Код: `config/log4net.config:75`, `config/log4net.config:87`, `config/log4net.config:98`.
3. Локализовать, где именно застыл поток:
- startup/login: `Program.cs:15`, `FrmLogIn.cs:35`;
- start-line: `FrmMain.cs:507`;
- network: `ProductLine.cs:122`, `ProductLine.cs:215`;
- business loop: `MyTimer.cs:92`;
- print: `MyTimer.cs:667`.

### 6.2 IR-01: Start не проходит, порт занят
Сигналы:
- ошибка в логе про занятый порт;
- линия не стартует.

Код: `FrmMain.cs:545`, `FrmMain.cs:547`.

Recovery:
1. Определить конфликтующий процесс на `<SERVER_PORT>`.
2. Остановить конфликтующий процесс/службу.
3. Повторить `Start`.

### 6.3 IR-02: DB недоступна / login fail
Сигналы:
- авторизация не проходит при корректных учетных данных;
- DB-исключения при старте/записи.

Код: `DBHelper.cs:349`, `DBHelper.cs:114`, `App.config:9`, `App.config:13`.

Recovery:
1. Проверить MySQL service и доступность `127.0.0.1:3306`.
2. Проверить `DbProfile` и ключи `connectMySql_prod/connectMySql_test` в `App.config`.
3. При startup fail-fast проверить текст ошибки и `logs/error`.
4. Повторить login и start.

### 6.4 IR-03: Станция не подключается / нелегитимный endpoint
Сигналы:
- станция отсутствует в `FrmNet`;
- не обновляются `status/quality`.

Код: `ProductLine.cs:171`, `ProductLine.cs:180`, `ProductLine.cs:204`, `FrmNet.cs:105`.

Recovery:
1. Сверить endpoint станции в таблице `station` и в фактической сети.
2. Проверить физическую сеть и доступность endpoint.
3. Переподключить станцию, затем проверить `connected == true`.

### 6.5 IR-04: Данные не пишутся / feedback неконсистентен
Сигналы:
- отсутствуют новые записи в station tables или `stationprocode/finalprintcode`;
- `feedBack` не соответствует фактическому состоянию.

Код: `MyTimer.cs:198`, `MyTimer.cs:236`, `DBHelper.cs:629`, `DBHelper.cs:511`, `DBHelper.cs:517`.

Recovery:
1. Проверить входной `status` станции в `FrmNet`.
2. Проверить целостность `productCode`/`barcode*` в пакете (protocol decode).
Код: `MyConvert.cs:29`, `MyConvert.cs:39`, `MyConvert.cs:46`.
3. Проверить SQL-ошибки и права в MySQL.
4. Выполнить controlled restart приложения.

### 6.6 IR-05: Печать не выполняется
Сигналы:
- изделие проходит, но этикетка отсутствует;
- проблемы на этапе `OP40`/`by2==42`.

Код: `MyTimer.cs:208`, `MyTimer.cs:217`, `MyTimer.cs:472`, `MyTimer.cs:703`.

Recovery:
1. Проверить, что выбран корректный `.prn` шаблон.
Код: `FrmMain.cs:518`, `FrmMain.cs:794`.
2. Проверить сетевую доступность `192.168.1.45:9100`.
3. Проверить, что PLC возвращает `by2` обратно в `0` после импульса печати.
Код: `MyTimer.cs:484`, `MyTimer.cs:486`.
4. Перезапустить приложение при застревании `setPrint`.

### 6.7 IR-06: Ошибка сверки партии при старте
Сигналы:
- сообщение о несоответствии скан-кодов и стандарт-кодов;
- старт блокируется до устранения mismatch.

Код: `FrmMain.cs:531`, `FrmMain.cs:533`, `FrmMain.cs:893`, `FrmMain.cs:906`.

Recovery:
1. Сверить справочники `standardcode` и `scancode` по выбранным `Product/Grade`.
2. Повторить старт.
3. Временно отключить проверку только по согласованной процедуре смены.

### 6.8 IR-07: Ошибки RFID bind/unbind
Сигналы:
- feedback `71/72/73/74/77`;
- отсутствует ожидаемая связка RFID-код.

Код: `MyTimer.cs:249`, `MyTimer.cs:254`, `MyTimer.cs:274`, `MyTimer.cs:590`, `DBHelper.cs:771`, `DBHelper.cs:795`.

Recovery:
1. Проверить, что `productCode` и `RFID` не пустые.
2. Проверить таблицу `rfidbind` по `productType/RFID/station`.
3. Повторить bind/unbind в штатной последовательности PLC.

## 7. Проверка после восстановления (exit criteria)
1. Успешный login и успешный start.
2. В `FrmNet` станции обновляются, `connected` стабилен.
3. За 1-2 изделия:
- есть записи в station tables;
- обновляется `stationprocode`;
- при финальном шаге обновляется `finalprintcode` и отрабатывает печать.
Код: `FrmNet.cs:105`, `DBHelper.cs:629`, `DBHelper.cs:511`, `DBHelper.cs:517`, `MyTimer.cs:667`.
4. В логах нет новых критичных ошибок за контрольный интервал.

## 8. Ограничения текущей версии
1. Нет полноценного graceful-stop runtime из UI (`Stop` не интегрирован в рабочую ветку).
Код: `FrmMain.cs:497`, `MyTimer.cs:87`.
2. IP принтера захардкожен (`192.168.1.45`).
Код: `MyTimer.cs:668`.
3. Логика `config/config.txt` для OP90 в `MyTimer` присутствует, но инициализация словаря в `FrmMain` отключена.
Код: `MyTimer.cs:515`, `FrmMain.cs:561`.

## 9. Связанные документы
- `docs/40-end-to-end-runtime-flow.md`
- `docs/60-error-catalog-and-troubleshooting.md`
- `docs/70-security-baseline.md`



