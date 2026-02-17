# PLC communication

## 1. Цель и границы
Этот документ описывает фактическую реализацию PLC communication в приложении `Review` (серверная TCP часть, протокол обмена, state machine статусов и диагностика).

В границах:
- transport: `TCP Socket` между сервером приложения и PLC-клиентами;
- connection lifecycle: `accept -> receive -> send -> disconnect -> reconnect`;
- data model и wire format: `StationData`, `MasterData`, `MyConvert`;
- operational semantics: `status`, `feedBack`, `heartbeat`, `timers`, `reconnect`.

Вне границ:
- PLC ladder logic;
- физический уровень сети;
- полная бизнес-логика БД вне методов, вызываемых в PLC pipeline;
- детализация печати как отдельного subsystem (кроме сигналов `by2=42`/`byI2`).

## 2. Компоненты и ответственность
- `FrmMain.cs`:
  - загрузка endpoint-ов (`ReadIPAndPort`) и запуск pipeline (`btnStart_Click`, `TechnologyDataConfig`);
  - heartbeat/run/enable флаги (`ConfigLineFlag`, `timer_Tick`).
- `ProductLine.cs`:
  - TCP server (`startLisen`), прием подключений (`AcceptSocketClient`), обмен кадрами (`ReceiveData`/`SendData`).
- `MyConvert.cs`:
  - сериализация/десериализация protocol payload (`ConvertReceiveData`, `ConvertSendData`).
- `MyTimer.cs`:
  - периодический polling и state machine для `status`/`feedBack` (`TimerFunc`, `DealNetUsedData`, `DealNetDownLineUsedData`).
- `DBHelper.cs`:
  - persistence, traceability, RFID bind/unbind и вспомогательные lookup-операции.
- `FrmNet.cs`:
  - runtime-инспекция входящих/исходящих полей по станциям.

## 3. Transport и connection lifecycle
1. При старте формы читаются endpoint-ы станции и сервера из таблицы `station`:
   - `FrmMain.cs:214` (`ReadIPAndPort`), схема таблицы: `lada_db.sql:624`.
2. При `Start` выбираются станции текущего продукта:
   - `FrmMain.cs:293` (`ConfigIPAndPort`) -> `DBHelper.selectStationList` (`DBHelper.cs:807`).
3. Создается TCP server:
   - `FrmMain.cs:364` -> `ProductLine` ctor;
   - `ProductLine.cs:122` (`startLisen`): `Bind`, `Listen(10)`, `ThreadPool.QueueUserWorkItem(AcceptSocketClient)`.
4. При входящем соединении:
   - `ProductLine.cs:147` (`AcceptSocketClient`);
   - дубликат `RemoteEndPoint` отклоняется (`ProductLine.cs:160`-`167`);
   - endpoint вне `dicAllStation` отклоняется (`ProductLine.cs:171`-`185`);
   - валидная станция получает `localIP`/`remoteIP`, `connected=true` (`ProductLine.cs:202`-`205`);
   - стартует приемный worker `ReceiveData` (`ProductLine.cs:206`).
5. В цикле приема:
   - `ReceiveTimeout=5000ms` (`ProductLine.cs:225`);
   - `Receive` -> `MyConvert.ConvertReceiveData` (`ProductLine.cs:226`-`227`);
   - после каждого кадра выполняется ответ `SendData -> MyConvert.ConvertSendData` (`ProductLine.cs:250`, `253`-`262`).
6. При ошибке/таймауте receive:
   - сокет удаляется из `clientList` и закрывается (`ProductLine.cs:240`-`246`);
   - `connected` переводится в `false` (с оговоркой в разделе failure modes).
7. Reconnect:
   - сервер не делает active reconnect;
   - reconnect инициирует PLC-клиент, сервер просто продолжает `Accept` в бесконечном цикле (`ProductLine.cs:153`).

## 4. Структуры данных и wire format
### 4.1 `StationData` (PLC -> Host)
Ключевые поля (`StationData.cs`):
- endpoint/runtime: `stationName`, `lastStationName`, `localIP`, `remoteIP`, `connected` (`StationData.cs:11`-`23`);
- telemetry/flags: `Estop`, `heart`, `alarm*` (`StationData.cs:30`-`46`);
- control data: `status`, `quality`, `by1`, `by2`, `RFID` (`StationData.cs:47`-`51`);
- payload: `result[30]`, `productCode`, `barcode2..barcode8` (`StationData.cs:52`-`60`).

### 4.2 `MasterData` (Host -> PLC)
Ключевые поля (`MasterData.cs`):
- host flags: `heart`, `run`, `enable`, `model` (`MasterData.cs:11`-`14`);
- source flags: `codeSource1..8` (`MasterData.cs:19`-`26`);
- command/feedback: `feedBack`, `type`, `leftOrRight`, `byI2` (`MasterData.cs:27`-`30`);
- outgoing strings: `productCode`, `barcode2..barcode8` (`MasterData.cs:31`-`38`).

### 4.3 `MyConvert` protocol mapping
`PLC -> Host` (`MyConvert.ConvertReceiveData`, `MyConvert.cs:11`):
- `byte[0]`: `Estop`, `heart`, `alarmMaterial`, `alarmQuality`, `alarmEquipment`, `alarmHelp`, `by06`, `by07`;
- `byte[1]`: `by10..by17`;
- `byte[2..3]`: `status` (`Int16`, big-endian via `BytesToInt162`);
- `byte[4..5]`: `quality`;
- `byte[6..7]`: `by1`;
- `byte[8..9]`: `by2`;
- `byte[10..29]`: `RFID` (20 bytes);
- `byte[50..129]`: `result[0..19]` (20 x `float`, шаг 4);
- `byte[130..169]`: `productCode`;
- `byte[170..449]`: `barcode2..barcode8` (7 x 40 bytes).

`Host -> PLC` (`MyConvert.ConvertSendData`, `MyConvert.cs:48`):
- длина кадра: `330 bytes`;
- `byte[0]`: `heart/run/enable/model/by04..by07`;
- `byte[1]`: `codeSource1..8`;
- `byte[2..3]`: `feedBack`;
- `byte[4..5]`: `type`;
- `byte[6..7]`: `leftOrRight`;
- `byte[8..9]`: `byI2`;
- `byte[10..329]`: `productCode + barcode2..barcode8` (8 x 40 bytes, `0x20` padding).

Важно:
- строки кодируются через `Encoding.Default` (`MyConvert.cs:213`, `268`);
- в конвертере нет проверки длины входного буфера до чтения offset-ов.

## 5. Словарь `status`/`feedback`
### 5.1 `StationData.status` и `StationData.by2` (что приходит от PLC)
- `status=0`: idle/reset state; сброс `Execution/haveSave`, `feedBack=0` (`MyTimer.cs:431`-`443`, `658`-`662`).
- `status=1`: pre-check/допуск на операцию (traceability проверка) (`MyTimer.cs:118` и `511`).
- `status=2`: сохранить результаты в БД (`MyTimer.cs:198` и `565`).
- `status=3`: RFID bind (`MyTimer.cs:242`, `572`).
- `status=4`: RFID unbind + возврат `productCode` в `MasterData.productCode` (`MyTimer.cs:265`, `595`).
- `status=21`/`31`: route decision для multi-station веток (`leftOrRight`) (`MyTimer.cs:292`-`363`).
- `status=22`/`24`/`32`/`34`: запись данных для multi-station (`MyTimer.cs:368`-`408`).
- `status=41`: scan-code final verification (`MyTimer.cs:410`-`425`).
- `status=82` (downline): запись в `OP80_2` (`MyTimer.cs:619`-`624`).

Сигналы через `by2`:
- `by2=42`: trigger печати, host выставляет `byI2=42` (`MyTimer.cs:472`-`481`).
- `by2=88`: bypass traceability save (`MyTimer.cs:447`-`456`, `628`-`636`).
- `by2=89`: alternate save path (`MyTimer.cs:460`-`467`, `641`-`646`).
- `by2=0`: reset печатного/сервисного состояния (`MyTimer.cs:484`-`488`, `653`-`657`).

### 5.2 `MasterData.feedBack` (что уходит в PLC)
Коды используются контекстно в `DealNetUsedData`/`DealNetDownLineUsedData`:
- `0`: idle/no response.
- `1`: допуск/OK (разрешение следующего шага).
- `2`: команда успешно обработана/данные сохранены.
- `3`: final scan verification failed (`status=41`).
- `41`: ожидание barcode verification (спец-ветка OP40).
- `71`: RFID bind: пустой `productCode`.
- `72`: RFID bind: пустой `RFID`.
- `73`: RFID unbind: для RFID нет привязанного `productCode`.
- `74`: RFID unbind: пустой `RFID`.
- `77`: RFID bind затронул >1 строку (downline branch).
- `91`: уже есть PASS на first-station.
- `92`: предыдущая релевантная станция имеет NG.
- `93`: уже есть PASS на текущей/последующей станции.
- `94`: отсутствует ожидаемая запись предыдущей станции/route mismatch.
- `-1`: deny (используется в разных сценариях, включая grade mismatch).
- `-2`: не удалось извлечь grade из входного кода (OP90 mode=1).

## 6. Тайминги, heartbeat, reconnect
- `FrmMain` UI timer: `1000ms`, на каждом tick выполняет `ConfigLineFlag` и переключает `MasterData.heart` при активном запуске (`FrmMain.Designer.cs:480`-`482`, `FrmMain.cs:309`-`355`, `597`-`604`).
- `MyTimer` polling timer: `200ms` (`MyTimer.cs:84`, `92`-`96`).
- `Socket.ReceiveTimeout`: `5000ms` (`ProductLine.cs:225`).
- `FrmNet` refresh timer: `500ms` (`FrmNet.Designer.cs:126`).
- В ветках `by2=42/88/89` есть `Thread.Sleep(3000)` внутри timer callback (`MyTimer.cs:455`, `466`, `481`, `635`, `646`), это добавляет задержку обработки.
- Reconnect модель passive: сервер всегда в `Accept` loop, PLC должен переподключаться самостоятельно (`ProductLine.cs:153`-`157`).

## 7. Failure modes (практически значимые)
1. `Bind`/`Listen` ошибка завершает приложение (`ProductLine.cs:135`-`139`).
2. Endpoint не из whitelist (`dicAllStation`) сразу закрывается как нелегальный (`ProductLine.cs:171`-`185`).
3. Duplicate `RemoteEndPoint` отвергается (`ProductLine.cs:160`-`167`).
4. `ReceiveTimeout`/сетевой exception приводит к disconnect и потере online-статуса (`ProductLine.cs:229`-`247`).
5. Нет проверки минимальной длины входного кадра в `ConvertReceiveData`; короткий/битый пакет вызывает exception и disconnect (offset до `410+40`) (`MyConvert.cs:11`-`46`).
6. `stationInfo` хранится как общий mutable field; при одновременных соединениях очистка `connected=false` может примениться не к той станции (сравнение через глобальный `stationInfo.socket`) (`ProductLine.cs:34`, `191`, `234`-`238`).
7. `status`-комментарий в `StationData` частично устарел относительно фактической state machine в `MyTimer` (например `21/31/41/82`) (`StationData.cs:47`, `MyTimer.cs:292`, `410`, `619`).
8. `feedBack=-1` используется в разных смыслах (deny на used path и grade mismatch на OP90), что осложняет однозначную диагностику на PLC стороне (`MyTimer.cs:192`, `527`, `540`).
9. Возможна задержка/конкуренция из-за `Thread.Sleep(3000)` в timer callback при периоде 200ms (`MyTimer.cs:84`, `455`, `466`, `481`).

## 8. Troubleshooting checklist
1. Проверить конфигурацию endpoint-ов в таблице `station` (`stationName`, `IP`, `port`) и корректность server-записи (`FrmMain.cs:214`-`228`, `lada_db.sql:624`-`629`).
2. Убедиться, что серверный порт свободен до старта (`MyTools.isPortAvalaible`) (`MyTools.cs:81`-`94`, `FrmMain.cs:545`-`548`).
3. Подтвердить запуск pipeline: вызваны `TechnologyDataConfig` и `MyTimer.Start` (`FrmMain.cs:551`-`554`).
4. В `FrmNet` проверить по станции: `connected`, `localIP/remoteIP`, `status`, `heart`, `feedBack`, `by2/byI2` (`FrmNet.cs:59`-`157`).
5. При частых отваливаниях проверить send cadence PLC (<5s) и размер кадра (для Rx требуется >=450 bytes) (`ProductLine.cs:225`, `MyConvert.cs:33`-`46`).
6. Если `status` завис не в `0`, проверить, формируется ли `feedBack` и возвращается ли PLC в idle (`MyTimer.cs:431`-`443`, `658`-`662`).
7. При RFID проблемах смотреть `feedBack` `71..74/77` и методы `RFIDBind/SelectRFIDBindingCode/clearRFIDBind` (`MyTimer.cs:249`-`285`, `579`-`615`, `DBHelper.cs:771`, `795`, `785`).
8. При `91/92/93/94` проверить последовательность станций и записи `stationprocode` (`MyTimer.cs:134`, `183`, `171`, `360`; `DBHelper.updateProCode/selectStationName/selectProResult`).
9. Проверить error/operation логи log4net: `logs/error/...`, `logs/operation/...`, `logs/NG_info/...` (`config/log4net.config:75`-`106`, `Utils/LogFile.cs:11`-`13`).

## 9. Быстрые ссылки на файлы и методы
- `FrmMain.cs:214` `ReadIPAndPort`
- `FrmMain.cs:293` `ConfigIPAndPort`
- `FrmMain.cs:309` `ConfigLineFlag`
- `FrmMain.cs:507` `btnStart_Click`
- `FrmMain.cs:597` `timer_Tick`
- `ProductLine.cs:122` `startLisen`
- `ProductLine.cs:147` `AcceptSocketClient`
- `ProductLine.cs:215` `ReceiveData`
- `ProductLine.cs:253` `SendData`
- `StationData.cs:9` `StationData`
- `MasterData.cs:9` `MasterData`
- `MyConvert.cs:11` `ConvertReceiveData`
- `MyConvert.cs:48` `ConvertSendData`
- `MyTimer.cs:81` `Start`
- `MyTimer.cs:92` `TimerFunc`
- `MyTimer.cs:98` `DealNetUsedData`
- `MyTimer.cs:492` `DealNetDownLineUsedData`
- `FrmNet.cs:59` `showAllStationData`
- `DBHelper.cs:807` `selectStationList`
- `DBHelper.cs:629` `SaveStationData`
- `DBHelper.cs:771` `RFIDBind`
- `DBHelper.cs:785` `clearRFIDBind`
- `DBHelper.cs:795` `SelectRFIDBindingCode`


