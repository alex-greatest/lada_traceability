# Error Catalog and Troubleshooting

## 1. Назначение
Документ фиксирует типовые инциденты в контуре `PLC ↔ Review ↔ MySQL ↔ Zebra`, признаки, проверку причин и порядок восстановления.

Применяется вместе с:
- `docs/50-operations-runbook.md`
- `docs/10-plc-communication.md`
- `docs/20-database-model-and-flows.md`
- `docs/30-zebra-printing.md`

## 2. Быстрый triage
1. Определить контур сбоя: `PLC`, `DB`, `Print`, `UI/Runtime`.
2. Проверить последние логи:
- `logs/operation/...`
- `logs/error/...`
- `logs/NG_info/...`
(см. `config/log4net.config:75`, `config/log4net.config:87`, `config/log4net.config:98`)
3. Проверить, где остановился pipeline:
- запуск линии: `FrmMain.cs:507`
- сетевой цикл: `ProductLine.cs:122`, `ProductLine.cs:215`
- обработчик тика: `MyTimer.cs:92`
- печать: `MyTimer.cs:667`
4. Локализовать проблему до одной точки отказа и применить runbook ниже.

## 3. Каталог инцидентов

### E-PLC-001: Нет подключения станции
Симптомы:
- станция не появляется в сетевом мониторинге
- данные не обновляются в `StationData`

Вероятные причины:
- некорректные IP/port в таблице `station`
- серверный порт занят/не прослушивается
- сетевой путь между станцией и сервером недоступен

Проверка:
1. Проверить загрузку сетевых параметров: `FrmMain.ReadIPAndPort` (`FrmMain.cs:214`).
2. Проверить запуск listener: `ProductLine.startLisen` (`ProductLine.cs:122`).
3. Проверить обработку accept: `ProductLine.AcceptSocketClient` (`ProductLine.cs:147`).

Recovery:
1. Исправить таблицу `station` (IP/port).
2. Устранить конфликт порта и перезапустить приложение.
3. Перезапустить станцию и подтвердить reconnect.


### E-PLC-002: Пакеты приходят, но status/feedback некорректны
Симптомы:
- линия работает нестабильно, неожиданные переходы состояний
- ошибки в валидации изделий

Вероятные причины:
- несоответствие бинарного протокола
- неверное декодирование/кодирование в `MyConvert`

Проверка:
1. Проверить разбор пакета: `MyConvert.ConvertReceiveData` (`ProductLine.cs:227`).
2. Проверить формирование ответа: `MyConvert.ConvertSendData` (`ProductLine.cs:259`).
3. Проверить бизнес-ветки статусов: `MyTimer.DealNetUsedData` (`MyTimer.cs:98`) и `MyTimer.DealNetDownLineUsedData` (`MyTimer.cs:492`).

Recovery:
1. Синхронизировать кодовую таблицу статусов с PLC-командой.
2. На время инцидента перевести линию в controlled stop.
3. После правок выполнить end-to-end smoke.

### E-DB-001: Ошибка подключения к MySQL
Симптомы:
- ошибки при login/запуске/записи данных

Вероятные причины:
- недоступен MySQL
- неверная строка подключения
- credentials invalid

Проверка:
1. Проверить `App.config` connection strings (`App.config:9`, `App.config:10`) и `DbProfile` (`App.config:13`).
2. Проверить доступ к `lada_db`.
3. Проверить что методы `DBHelper`/`SqlSugarHelper` могут выполнить простой select.

Recovery:
1. Восстановить доступность MySQL.
2. Проверить и обновить connection settings.
3. Перезапустить приложение и проверить login + start flow.

### E-DB-002: Ошибки SQL в runtime
Симптомы:
- падения или пропуски записи в таблицы
- отсутствуют строки в `finalprintcode`/`stationprocode`

Вероятные причины:
- некорректный SQL-конструктор в `DBHelper`
- невалидные значения из station payload

Проверка:
1. Проверить вызовы:
- `DBHelper.saveToFinalTable` (`DBHelper.cs:517`)
- `DBHelper.updateStationData_20` (`DBHelper.cs:755`)
- `DBHelper.updateStationData_30` (`DBHelper.cs:762`)
2. Проверить соответствие схемы `lada_db.sql`.

Recovery:
1. Зафиксировать failing SQL.
2. Выполнить ручную проверку данных payload.
3. После восстановления выполнить контрольную обработку тестового изделия.

### E-DB-003: Некорректный DbProfile / отсутствует profile connection string
Симптомы:
- приложение не доходит до `FrmLogIn`, показывает `Startup Error`;
- в `logs/error` фиксируется ошибка конфигурации профиля БД.

Вероятные причины:
- `DbProfile` отсутствует, пустой или имеет значение вне `prod/test`;
- отсутствует `connectMySql_prod` или `connectMySql_test`;
- ключ подключения есть, но строка подключения пустая.

Проверка:
1. Проверить `App.config`:
- `appSettings/DbProfile`;
- `connectionStrings/connectMySql_prod`;
- `connectionStrings/connectMySql_test`.
2. Проверить startup-check в `Program.Main` и резолвер:
- `Program.cs:22`;
- `Utils/DbProfileResolver.cs:36`;
- `Utils/DbProfileResolver.cs:59`.

Recovery:
1. Исправить `DbProfile` на `prod` или `test`.
2. Восстановить непустой connection string для выбранного профиля.
3. Перезапустить приложение.
4. Подтвердить, что логин доступен и DB-операции проходят smoke-проверку.

### E-PRN-001: Принтер недоступен
Симптомы:
- изделие проходит, но этикетка не печатается

Вероятные причины:
- недоступен принтер IP
- Zebra service/сетевой канал недоступен
- проблемы с отправкой `.prn`

Проверка:
1. Проверить вызов печати: `MyTimer.printCode` (`MyTimer.cs:667`).
2. Проверить Zebra connect/send:
- `TcpConnection(...)` (`MyTimer.cs:703`)
- `ZebraPrinterFactory.GetInstance` (`MyTimer.cs:705`)
- `printer.SendFileContents` (`MyTimer.cs:706`)
3. Проверить наличие шаблона `.prn` и корректность подстановки.

Recovery:
1. Восстановить доступность принтера в сети.
2. Проверить/перевыбрать шаблон в UI (`FrmMain`), повторить печать.
3. При необходимости выполнить ручную печать тестом (`zebra_print_test`).

### E-PRN-002: Печать не триггерится повторно
Симптомы:
- единичная печать прошла, дальше печать не запускается

Вероятные причины:
- флаг `setPrint` не сброшен (ожидание `by2 == 0`)

Проверка:
1. Проверить ветки `by2` в `MyTimer.DealNetUsedData` (`MyTimer.cs:444+`).

Recovery:
1. Убедиться, что PLC корректно возвращает `by2` в `0` после печати.
2. При stuck-состоянии выполнить controlled restart линии.

### E-UI-001: Не удается стартовать линию
Симптомы:
- `btnStart` показывает предупреждения, линия не стартует

Вероятные причины:
- не выбран продукт/грейд
- не выбран шаблон печати
- провал верификации кодов

Проверка:
- `FrmMain.btnStart_Click` (`FrmMain.cs:507`)
- `CodeVerification` (`FrmMain.cs:890`)
- проверки сообщений в `FrmMain.cs:510`, `FrmMain.cs:519`, `FrmMain.cs:533`

Recovery:
1. Выбрать корректные product/grade/template.
2. Устранить mismatch в `standardcode/scancode`.
3. Повторить запуск.

## 4. Эскалация и артефакты
При эскалации в разработку приложить:
1. Timestamp и смена/линия.
2. Фрагменты из `logs/operation` и `logs/error`.
3. Симптом + шаги воспроизведения.
4. Последний известный `productCode`.
5. Для PLC: station IP/port и expected status.
6. Для печати: template path и статус подключения принтера.

## 5. Правило обновления каталога
Любой новый тип инцидента обязан быть добавлен в этот документ в формате:
- ID
- Symptoms
- Likely causes
- Verification
- Recovery
- Escalation artifacts


