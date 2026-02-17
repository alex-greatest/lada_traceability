# Zebra-печать в LADA Review: шаблоны, триггеры, подстановка и recovery

## 1. Контур печати (as-is)
Печать Zebra в runtime выполняется из `MyTimer`:
- БД-часть: получение текущего шаблонного набора из таблицы `printcode` через `DBHelper.selectPrintCode` (`DBHelper.cs:919`).
- Файл-часть: чтение исходного `.prn`, подстановка значений, запись промежуточного файла `D:\Marking Lable Template\11.prn` (`MyTimer.cs:669`, `MyTimer.cs:670`, `MyTimer.cs:693`).
- Zebra-часть: TCP-подключение и отправка файла через SDK (`MyTimer.cs:703`, `MyTimer.cs:706`).

Ключевой метод: `MyTimer.printCode(List<string> printcode)` (`MyTimer.cs:667`).

## 2. Источники шаблонов: `printcode` и `.prn`

### 2.1 Логический шаблон в БД (`printcode`)
Источник полей для подстановки формируется и хранится в модели `printCode`:
- Модель: `Model/printCode.cs:11`.
- Поля: `template1..template4`, `proType1`, `proType2`, `productCode` (`Model/printCode.cs:16-24`).
- Чтение в runtime: `DBHelper.selectPrintCode` -> `select * from printcode ...` (`DBHelper.cs:919-922`).
- Обновление кода: `DBHelper.updatePrintCode` (`DBHelper.cs:929-933`).

Настройка/инициализация шаблонных полей выполняется в форме настроек:
- `FrmSetPrintCode.saveOrUpdatePrintCode` (`FrmSetPrintCode.cs:91`).
- Генерация масок:
- `template1 = new string('1', ...)` (`FrmSetPrintCode.cs:111`).
- `template3 = new string('3', ...)` (`FrmSetPrintCode.cs:112`).
- `template4 = new string('4', ...)` (`FrmSetPrintCode.cs:113`).
- Upsert записи `printcode`: `SqlSugarHelper.printCodeUpOrInEntity` (`Repository/SqlSugarHelper.cs:309`).

Фактическая индексная адресация в runtime (по `select *`) используется как:

| Индекс `printcode[]` | Значение (по модели `printCode`) | Где используется |
|---|---|---|
| `2` | `template1` | `MyTimer.cs:682` |
| `4` | `template3` | `MyTimer.cs:684` |
| `5` | `template4` | `MyTimer.cs:685` |
| `6` | `proType1` | `MyTimer.cs:682`, `MyTimer.cs:685` |
| `7` | `proType2` | `MyTimer.cs:684`, `MyTimer.cs:685` |
| `8` | `productCode` | `MyTimer.cs:684`, `MyTimer.cs:685` |

### 2.2 Физический шаблон `.prn`
Физический `.prn` выбирается оператором в UI и передается в runtime:
- UI-контролы выбора: `FrmMain.Designer.cs:275`, `FrmMain.Designer.cs:282`, `FrmMain.Designer.cs:294`.
- Диалог выбора файла: `FrmMain.button1_Click` (`FrmMain.cs:769`).
- Фильтр только `.prn`: `FrmMain.cs:781`.
- Сохранение выбранного полного пути: `proCodeTemplate = openFileDialog.FileName` (`FrmMain.cs:794`).
- Передача пути в `MyTimer`: `new MyTimer(..., proCodeTemplate)` (`FrmMain.cs:552`).

## 3. Где именно выбирается шаблон
Единственная рабочая точка выбора `.prn` для runtime-печати:
- `FrmMain.button1_Click` (`FrmMain.cs:769-799`).
- При запуске проверяется, что шаблон выбран: `if (proTemplate.Text == "")` (`FrmMain.cs:518`).

Примечание:
- В `FrmMain` есть дублирующий метод `printCode(...)` (`FrmMain.cs:720`), но в кодовой базе нет его вызовов; рабочий путь печати идет через `MyTimer.printCode(...)`.

## 4. Триггеры печати

### 4.1 Автоматический триггер на OP40 (основной production-путь)
В `MyTimer.DealNetUsedData`:
- Условие статуса: `stationData.status == 2` (`MyTimer.cs:198`).
- Фильтр станции: `stationName.Equals("OP40")` (`MyTimer.cs:208`).
- Перед печатью сохраняется запись в `finalprintcode`: `DBHelper.saveToFinalTable(...)` (`MyTimer.cs:213`).
- Печать выполняется при `quality != 2`: `printCode(printCodeList)` (`MyTimer.cs:215-217`).

### 4.2 PLC-команда по `by2 == 42` (ручной/служебный триггер)
- Ветка триггера: `stationData.by2 == 42 && !setPrint` (`MyTimer.cs:472`).
- Защита от повтора: флаг `setPrint` (`MyTimer.cs:29`, `MyTimer.cs:474`) и reset при `by2 == 0` (`MyTimer.cs:484-487`).
- Печать + обновление `productCode`: `MyTimer.cs:476`, `MyTimer.cs:479`.

### 4.3 Триггер финальной валидации кода (`status == 41`)
- Ветка: `stationData.status == 41` (`MyTimer.cs:410`).
- Сверка сканированного кода с ожидаемым `finalprintcode.printCode`: `DBHelper.selectFinalPrintCode` (`MyTimer.cs:413`, `DBHelper.cs:495`).
- При успехе: обновление следующего `printcode.productCode` и `finalprintcode.scanCode` (`MyTimer.cs:417-418`, `DBHelper.cs:550`).
- При несовпадении: feedback `3` (`MyTimer.cs:423-424`).

## 5. Алгоритм подстановки в `.prn`
Алгоритм в `MyTimer.printCode` (`MyTimer.cs:667`):
- Читается исходный `.prn` построчно (`MyTimer.cs:675`, `MyTimer.cs:680`).
- Выполняются замены:
- `^FD{template1}^FS` -> `^FD{proType1}^FS` (`MyTimer.cs:682`).
- `^FD{template3}^FS` -> `^FD{proType2} {productCode}^FS` (`MyTimer.cs:684`).
- `^FD,{template4}^FS` -> `^FD,{proType1} {proType2} {productCode}^FS` (`MyTimer.cs:685`).
- Результат пишется в `D:\Marking Lable Template\11.prn` (`MyTimer.cs:670`, `MyTimer.cs:693-699`).
- Далее отправка файла в принтер (`MyTimer.cs:706`).

Замечание:
- `template2` в runtime-заменах не используется; строка замены закомментирована (`MyTimer.cs:683`).

## 6. Zebra сеть и SDK
Текущая реализация:
- SDK namespaces: `using Zebra.Sdk.Comm`, `using Zebra.Sdk.Printer` (`MyTimer.cs:14-15`, `FrmMain.cs:16-17`).
- Библиотеки подключены в проекте через `Zebra.Printer.Card.SDK.2.15.2634` (`LADA.csproj:67-80`).
- Подключение: `TcpConnection(printerIp, TcpConnection.DEFAULT_ZPL_TCP_PORT)` (`MyTimer.cs:703`).
- IP принтера жестко задан: `192.168.1.45` (`MyTimer.cs:668`).
- Отправка: `ZebraPrinterFactory.GetInstance(connection)` + `printer.SendFileContents(filePath2)` (`MyTimer.cs:705-706`).

Тестовый стенд печати:
- `zebra_print_test/zebra_print_test/Form1.cs:23`.

## 7. Failure modes (текущее состояние)

### F-01: Схема БД не совпадает с кодом
- Код использует `finalprintcode.scanCode` (`DBHelper.cs:504`, `DBHelper.cs:550`).
- В `lada_db.sql` у `finalprintcode` есть `boxCode`, но нет `scanCode` (`lada_db.sql:29`).
- Таблица `printcode`, используемая кодом (`DBHelper.cs:920`), в `lada_db.sql` отсутствует.

### F-02: Ошибка печати гасится в `Console` без обратной связи в PLC/UI
- Исключения в `MyTimer.printCode` только логируются в `Console.WriteLine` (`MyTimer.cs:711-713`).
- Нет retry/backoff и нет явного `feedBack` по ошибке печати в этой ветке.

### F-03: Жестко зашитые сетевые/файловые параметры
- IP принтера фиксирован (`MyTimer.cs:668`).
- Промежуточный `.prn` путь фиксирован (`MyTimer.cs:670`).
- При отсутствии каталога `D:\Marking Lable Template\` печать падает на записи файла.

### F-04: Риск рассинхронизации/неинкрементируемого кода
- Генерация следующего кода: `utils.GenerateNextCode` (`Utils/Utils.cs:33`).
- Выражение `sequence = init ? sequence : sequence++;` (`Utils/Utils.cs:52`) не увеличивает sequence как ожидается.
- Это влияет на `DBHelper.updatePrintCode` в `FrmMain.btnStart_Click` и `MyTimer` (`FrmMain.cs:516`, `MyTimer.cs:417`, `MyTimer.cs:479`).

### F-05: Неполная гарантия инициализации `printCodeList` перед `status == 41`
- В `status == 41` используется `printCodeList[8]` (`MyTimer.cs:416`).
- Если ветки, заполняющие `printCodeList`, не отработали ранее, возможен runtime-exception.

### F-06: Неиспользуемый дублирующий код печати в `FrmMain`
- Метод `FrmMain.printCode(...)` существует (`FrmMain.cs:720`), но в вызовах не используется.
- Повышает риск дивергенции логики при будущих изменениях.

## 8. Операционные проверки (перед сменой и в runtime)

### 8.1 Перед запуском линии
- Убедиться, что выбран `.prn` шаблон через UI (`FrmMain.cs:769`, `FrmMain.cs:781`).
- Убедиться, что `proTemplate` не пустой (`FrmMain.cs:518`).
- Проверить наличие записи `printcode` для модели и корректность полей `template1/3/4`, `proType1/2`, `productCode` (`DBHelper.cs:919`, `Model/printCode.cs:16-24`).
- Проверить сетевую доступность `192.168.1.45:9100`.
- Проверить доступность записи в `D:\Marking Lable Template\`.

### 8.2 Во время работы
- Мониторить ветки OP40/status=2 и by2=42 (`MyTimer.cs:198`, `MyTimer.cs:472`).
- Контролировать, что после by2=42 приходит reset `by2 == 0` (`MyTimer.cs:484`).
- Контролировать запись в `finalprintcode` перед печатью (`MyTimer.cs:213`).
- Контролировать статусную валидацию `status == 41` и `feedBack` (`MyTimer.cs:410-424`).

## 9. Recovery

### R-01: Печать не идет вообще
1. Проверить выбран ли шаблон в UI (`FrmMain.cs:518`, `FrmMain.cs:769`).
2. Проверить доступ к файлу шаблона и каталогу `D:\Marking Lable Template\` (`MyTimer.cs:669-670`, `MyTimer.cs:693`).
3. Проверить TCP до принтера и порт 9100 (`MyTimer.cs:703`).
4. Выполнить тест прямой отправки через `zebra_print_test` (`zebra_print_test/zebra_print_test/Form1.cs:23`).

### R-02: Печать однократная, затем не повторяется
1. Проверить, сбрасывается ли `by2` в `0` после команды `42` (`MyTimer.cs:472`, `MyTimer.cs:484`).
2. Проверить, не залип ли `setPrint` (`MyTimer.cs:29`, `MyTimer.cs:474`).

### R-03: Скан-контроль (`status==41`) не проходит
1. Проверить наличие строки в `finalprintcode` с ожидаемым `printCode` (`DBHelper.cs:495`, `MyTimer.cs:413`).
2. Проверить соответствие схемы БД полям кода (`lada_db.sql:24`, `DBHelper.cs:550`).
3. Если mismatch схемы подтвержден, синхронизировать DDL с кодом до продолжения смены.

### R-04: Рассинхрон/дубли `productCode`
1. Проверить поведение `GenerateNextCode` (`Utils/Utils.cs:33`, `Utils/Utils.cs:52`).
2. Проверить последние `update printcode set productCode = ...` (`DBHelper.cs:932`).
3. При инциденте вручную выровнять `printcode.productCode` в БД и повторить контролируемый запуск.

## 10. Карта исходников (быстрые ссылки)
- Выбор `.prn` в UI: `FrmMain.button1_Click` (`FrmMain.cs:769`).
- Запуск линии и передача пути шаблона в runtime: `FrmMain.btnStart_Click` (`FrmMain.cs:507`, `FrmMain.cs:552`).
- Runtime-триггеры печати и валидации: `MyTimer.DealNetUsedData` (`MyTimer.cs:198`, `MyTimer.cs:410`, `MyTimer.cs:472`).
- Подстановка и отправка Zebra: `MyTimer.printCode` (`MyTimer.cs:667`).
- БД для печати: `DBHelper.selectPrintCode` (`DBHelper.cs:919`), `DBHelper.saveToFinalTable` (`DBHelper.cs:517`), `DBHelper.updatePrintCode` (`DBHelper.cs:929`).
- Настройка `printcode`: `FrmSetPrintCode.saveOrUpdatePrintCode` (`FrmSetPrintCode.cs:91`).
- SDK зависимости: `LADA.csproj:67-80`.


