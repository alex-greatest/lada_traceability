# 20. Модель БД и потоки данных

## 1) Область и источники
Документ описывает фактический data-access в проекте по состоянию кода в репозитории. Источники:
- `App.config:6`-`App.config:10` (connection strings).
- `Repository/DBcontext.cs:15`-`Repository/DBcontext.cs:41` (SqlSugar profile).
- `DBHelper.cs:18`-`DBHelper.cs:1028` (ADO.NET MySQL слой + бизнес-методы).
- `Repository/SqlSugarHelper.cs:15`-`Repository/SqlSugarHelper.cs:373` (ORM-методы).
- `MyTimer.cs:98`-`MyTimer.cs:658` (основные write/read сценарии линии).
- `FrmReview.cs:166`-`FrmReview.cs:371`, `FrmReviewData.cs:125`-`FrmReviewData.cs:392` (чтение/конфигурация trace-отчетов).
- `lada_db.sql:24`-`lada_db.sql:657` (дамп схемы).

## 2) Connection profile
| Параметр | Значение | Где в коде |
|---|---|---|
| DBMS | MySQL | `App.config:9`, `Repository/DBcontext.cs:22` |
| БД | `lada_db` | `App.config:9` |
| Хост/порт | `127.0.0.1:3306` | `App.config:9` |
| Учетные данные | `root / 123456` (в открытом виде) | `App.config:9` |
| Пул соединений | `Pooling=true` | `App.config:9` |
| ADO.NET драйвер | `MySql.Data 8.0.32` | `App.config:37`, `LADA.csproj:56` |
| ORM | `SqlSugar 5.1.4.198` | `LADA.csproj:85`, `packages.config:10` |
| Command timeout | `5000` (часть методов `DBHelper`), `30s` (SqlSugar ADO) | `DBHelper.cs:53`, `DBHelper.cs:73`, `Repository/DBcontext.cs:41` |
| Профиль SQL Server | Есть legacy-строка, фактически не используется в LADA-потоках | `App.config:8` |

Дополнительно:
- `DBHelper` открывает новое соединение почти на каждый вызов (`new MySqlConnection(ConnStr)`), транзакции по умолчанию автокоммитные: `DBHelper.cs:29`, `DBHelper.cs:50`, `DBHelper.cs:93` и др.
- `SqlSugarScope` работает как singleton с `IsAutoCloseConnection = true`: `Repository/DBcontext.cs:19`-`Repository/DBcontext.cs:23`.
- SQL логируется в `Console` через `OnLogExecuting`: `Repository/DBcontext.cs:28`.

## 3) Ключевые таблицы

### 3.1 Таблицы, подтвержденные в `lada_db.sql`
| Таблица | Назначение | Ключевые колонки | Где объявлена |
|---|---|---|---|
| `stationprocode` | Агрегированное состояние изделия по линии | `productCode`, `productResult`, `stationName`, `productType`, `productName` | `lada_db.sql:636` |
| `finalprintcode` | Финальная связка кодов для печати/трассировки | `printCode`, `productCode`, `fixedEndCode`, `innerCode`, `result`, `time` | `lada_db.sql:24` |
| `product` | Маршрут/конфигурация линии (station1..station14) | `productName`, `productID`, `station1..station14` | `lada_db.sql:546` |
| `reviewdata` | Настройка полей для отчетов по станциям | `productName`, `stationName`, `data1..data20`, `barcode1..barcode8` | `lada_db.sql:571` |
| `rfidbind` | Привязка RFID к текущему `productCode` | `productType`, `RFID`, `station`, `productCode`, `last` | `lada_db.sql:610` |
| `station` | Справочник станций и сетевых параметров | `stationName`, `IP`, `port` | `lada_db.sql:624` |
| `usertable` | Пользователи/уровни доступа | `userName`, `userPwd`, `userLevel` | `lada_db.sql:651` |
| `op05/op10/.../op120` | Таблицы данных станций (одинаковый wide-формат) | `productCode`, `data1..data20`, `barcode1..barcode8`, `result` | `lada_db.sql:42`, `lada_db.sql:84`, `lada_db.sql:126`, `lada_db.sql:168`, `lada_db.sql:210`, `lada_db.sql:252`, `lada_db.sql:294`, `lada_db.sql:336`, `lada_db.sql:378`, `lada_db.sql:420`, `lada_db.sql:462`, `lada_db.sql:504` |

### 3.2 Таблицы, используемые кодом, но отсутствующие в `lada_db.sql`
| Таблица | Где используется |
|---|---|
| `printcode` | `DBHelper.cs:919`, `DBHelper.cs:929`, `FrmSetPrintCode.cs:40` |
| `RandomInnerId` | `DBHelper.cs:937`, `DBHelper.cs:945`, `DBHelper.cs:950` |
| `OP80_2` (через параметр `tableName`) | `MyTimer.cs:620`, `MyTimer.cs:643`, `DBHelper.cs:819` |
| `op60_2` | `DBHelper.cs:814` |
| `AssistantStation` | `DBHelper.cs:343`, `FrmAssistantStation.cs:78` |
| `Operator`, `Barcode`, `RS`, `moreStation` | `FrmGroup.cs:27`, `FrmBarcode.cs:110`, `FrmRS.cs:59`, `moreStation.cs:68` |

Это прямой индикатор дрейфа схемы между кодом и дампом.

## 4) Матрица method -> table

### 4.1 Ядро traceability (основной pipeline)
| Метод | Таблицы | Тип доступа | Где вызывается |
|---|---|---|---|
| `DBHelper.insertStationProcode` (`DBHelper.cs:554`) | `stationprocode` | `INSERT` | `MyTimer.cs:204` |
| `DBHelper.updateProCode` (`DBHelper.cs:511`) | `stationprocode` | `UPDATE` | `MyTimer.cs:224`, `MyTimer.cs:400` |
| `DBHelper.selectProResult` (`DBHelper.cs:561`, `DBHelper.cs:571`) | `stationprocode`, `op20`, динамически `stationName` | `SELECT` | `MyTimer.cs:299` |
| `DBHelper.selectStationName` (`DBHelper.cs:583`) | `stationprocode`, динамически `stationName` | `SELECT` | `MyTimer.cs:301` |
| `SqlSugarHelper.selectPro` (`Repository/SqlSugarHelper.cs:68`) | `stationprocode`, fallback `OP20`, `OP30` | `SELECT` | `MyTimer.cs:124` |
| `DBHelper.SaveStationData` (`DBHelper.cs:629`) | динамическая станция (`sd.stationName`) | `INSERT` | `MyTimer.cs:236`, `MyTimer.cs:385`, `MyTimer.cs:451` |
| `DBHelper.updateStationData` (`DBHelper.cs:711`) | динамическая станция (`sd.stationName`) | `UPDATE` | `MyTimer.cs:567` |
| `DBHelper.updateStationData_20` (`DBHelper.cs:755`) | динамическая станция (`sd.stationName`) | `UPDATE` | `MyTimer.cs:389` |
| `DBHelper.updateStationData_30` (`DBHelper.cs:762`) | динамическая станция (`sd.stationName`) | `UPDATE` | `MyTimer.cs:397` |
| `DBHelper.checkSingleStation` (`DBHelper.cs:707`) | динамическая станция (`stationName`) | `SELECT` | `MyTimer.cs:549` |
| `DBHelper.selectPCode` (`DBHelper.cs:485`) | динамическая станция (`OP20/OP30/...`) | `SELECT` | `MyTimer.cs:138`, `MyTimer.cs:141`, `MyTimer.cs:402` |
| `DBHelper.selectStationProcode` (`DBHelper.cs:490`) | `stationprocode` | `SELECT` | `MyTimer.cs:375` |
| `DBHelper.saveToFinalTable` (`DBHelper.cs:517`) | `op20`, `op30`, `finalprintcode` | `SELECT + INSERT` | `MyTimer.cs:213` |
| `DBHelper.selectFinalPrintCode` (`DBHelper.cs:495`) | `finalprintcode` | `SELECT` | `MyTimer.cs:413` |
| `DBHelper.selectScanCode` (`DBHelper.cs:504`) | `finalprintCode` | `SELECT` | `MyTimer.cs:144` |
| `DBHelper.updateScanCode` (`DBHelper.cs:550`) | `finalprintcode` | `UPDATE` | `MyTimer.cs:418` |
| `DBHelper.selectPrintCode` (`DBHelper.cs:919`) | `printcode` | `SELECT` | `MyTimer.cs:210`, `FrmMain.cs:515` |
| `DBHelper.updatePrintCode` (`DBHelper.cs:929`) | `printcode` | `UPDATE` | `MyTimer.cs:417`, `MyTimer.cs:479`, `FrmMain.cs:516` |
| `DBHelper.RFIDBind` (`DBHelper.cs:771`) | `rfidbind` | `SELECT + INSERT/UPDATE` | `MyTimer.cs:258`, `MyTimer.cs:588` |
| `DBHelper.SelectRFIDBindingCode` (`DBHelper.cs:795`) | `rfidbind` | `SELECT` | `MyTimer.cs:271`, `MyTimer.cs:601` |
| `DBHelper.clearRFIDBind` (`DBHelper.cs:785`) | `rfidbind` | `UPDATE` | `MyTimer.cs:284`, `MyTimer.cs:614` |
| `DBHelper.save80_2Data` (`DBHelper.cs:819`) | динамическая таблица (`OP80_2`) | `INSERT` | `MyTimer.cs:620`, `MyTimer.cs:643` |

### 4.2 Reporting/мастер-данные (SQL напрямую из форм)
| Метод | Таблицы | Тип доступа |
|---|---|---|
| `FrmReview.Search_Click` (`FrmReview.cs:166`) | `finalprintcode`, `reviewdata`, станции `OPxx` | `SELECT` (динамический SQL, join chain) |
| `FrmReview.searchAllStation` (`FrmReview.cs:256`) | `reviewdata` + `finalprintcode` + станции | `SELECT` |
| `FrmReviewData.ShowReview` (`FrmReviewData.cs:170`) | `ReviewData` | `SELECT` |
| `FrmReviewData.btnOK_Click` (`FrmReviewData.cs:221`) | `ReviewData` | `INSERT/UPDATE` |
| `FrmMain.FillcomProduct` (`FrmMain.cs:237`) | `Product` | `SELECT` |
| `FrmMain.ConfigIPAndPort` (`FrmMain.cs:293`) | `product` (через `selectStationList`) | `SELECT` |
| `FrmMain.getProductType` (`FrmMain.cs:580`) | `Product` | `SELECT` |
| `FrmUser.showUser` (`FrmUser.cs:94`) | `UserTable` | `SELECT` |
| `FrmStation.ShowStation` (`FrmStation.cs:22`) | `Station` | `SELECT` |
| `FrmProduct.ShowProduct` (`FrmProduct.cs:107`) | `Product` | `SELECT` |

### 4.3 SqlSugar-ветка (ORM и конфигурационные таблицы)
| Метод/операция | Таблицы (по entity или `.AS(...)`) | Где вызывается |
|---|---|---|
| `SqlSugarHelper.GetDataList<printCode>` | `printCode` | `FrmSetPrintCode.cs:40` |
| `SqlSugarHelper.printCodeUpOrInEntity` | `printCode` | `FrmSetPrintCode.cs:115` |
| `SqlSugarHelper.GetColumnList<product,string>` | `product` | `FrmSetPrintCode.cs:73` |
| `SqlSugarHelper.GetDataList<tablegrade>` / `UpdateByCondition<tablegrade>` | `tablegrade` | `FrmMain.cs:252`, `UI/FormGradeNumber.cs:96` |
| `SqlSugarHelper.GetDataList<standardcode>` / `ParaMetersUpOrInEntity` | `standardcode` | `UI/FrmStandCode.cs:153`, `UI/FrmStandCode.cs:238` |
| `SqlSugarHelper.GetDataList<scancode>` / `DeleteByCondition<scancode>` | `scancode` | `UI/FrmVerifySCode.cs:169`, `UI/FrmVerifySCode.cs:214` |
| `SqlSugarHelper.GetDataList<codeverification>` / `InsertEntity<codeverification>` | `codeverification` | `UI/FormGradeNumber.cs:154`, `UI/FormGradeNumber.cs:167` |
| `SqlSugarHelper.SaveStationData(...).AS(StationData.stationName)` | динамическая таблица станции | в репозитории определен, в текущем коде не вызван (`Repository/SqlSugarHelper.cs:15`) |

## 5) Потоки записи

### 5.1 Main line: `status == 2` (базовый производственный write-path)
1. При первом посту создается агрегатная запись в `stationprocode` (`insertStationProcode`)  
   Ссылка: `MyTimer.cs:198`, `MyTimer.cs:204`, `DBHelper.cs:554`.
2. Для следующих постов обновляется `stationprocode.productResult/stationName/time` (`updateProCode`)  
   Ссылка: `MyTimer.cs:224`, `DBHelper.cs:511`.
3. Для OP40 пишется финальная запись в `finalprintcode` через join `op20 + op30` (`saveToFinalTable`)  
   Ссылка: `MyTimer.cs:213`, `DBHelper.cs:517`.
4. В любом случае пишется запись конкретной станции в таблицу `sd.stationName` (`SaveStationData`)  
   Ссылка: `MyTimer.cs:236`, `DBHelper.cs:629`.

### 5.2 Split-поток OP20/OP30 (статусы 22/24/32/34)
1. Для веток OP30 сначала нормализуется `productCode` через `stationprocode`/`op20.barcode4`  
   Ссылка: `MyTimer.cs:373`, `MyTimer.cs:375`, `MyTimer.cs:378`.
2. `22`/`32` -> `SaveStationData`, `24` -> `updateStationData_20`, `34` -> `updateStationData_30`.  
   Ссылка: `MyTimer.cs:381`-`MyTimer.cs:397`, `DBHelper.cs:629`, `DBHelper.cs:755`, `DBHelper.cs:762`.
3. Затем обновляется агрегат в `stationprocode` (`updateProCode`).  
   Ссылка: `MyTimer.cs:400`.

### 5.3 RFID поток (статусы 3/4)
1. Bind (`status==3`): `rfidbind` update или insert.  
   Ссылка: `MyTimer.cs:242`, `DBHelper.cs:771`.
2. Unbind (`status==4`): чтение `productCode` по RFID и очистка текущей привязки (`last` сохраняется).  
   Ссылка: `MyTimer.cs:265`, `DBHelper.cs:795`, `DBHelper.cs:785`.

### 5.4 Финальная валидация print code (`status==41`)
1. Проверка наличия `printCode` в `finalprintcode`.  
   Ссылка: `MyTimer.cs:410`, `DBHelper.cs:495`.
2. Инкремент `printcode.productCode` и фиксация `finalprintcode.scanCode`.  
   Ссылка: `MyTimer.cs:417`-`MyTimer.cs:418`, `DBHelper.cs:929`, `DBHelper.cs:550`.

### 5.5 Offline/доп. поток
- `status==82` и `by2==89` пишут в `OP80_2` через параметризованный insert-метод.  
  Ссылка: `MyTimer.cs:619`, `MyTimer.cs:641`, `DBHelper.cs:819`.

## 6) Потоки чтения

### 6.1 Проверка допуска на станцию (`status==1`, `21`, `31`)
- Чтение агрегата по изделию (`stationprocode`) + fallback через `OP20.barcode4`/`OP30.barcode3`.
- Используются `SqlSugarHelper.selectPro` и/или `DBHelper.selectProResult`, `DBHelper.selectStationName`.
- Ссылка: `MyTimer.cs:118`, `MyTimer.cs:292`, `Repository/SqlSugarHelper.cs:68`, `DBHelper.cs:561`, `DBHelper.cs:583`.

### 6.2 Report/query поток
- `FrmReview` берет конфигурацию полей из `reviewdata`, строит динамические select/join к `finalprintcode` и станциям.
- Для OP100/OP110 используется join по `fin.fixedEndCode`/`fin.innerCode` (не по `productCode`).
- Ссылка: `FrmReview.cs:256`, `FrmReview.cs:294`, `FrmReview.cs:298`, `FrmReview.cs:336`.

### 6.3 Конфигурация отчетности
- `FrmReviewData` читает/пишет `ReviewData`, а набор доступных станций берет из `Product` и `station`.
- Ссылка: `FrmReviewData.cs:140`, `FrmReviewData.cs:160`, `FrmReviewData.cs:170`, `FrmReviewData.cs:221`.

## 7) Transactional behavior

### 7.1 Что реально транзакционно
- В проекте есть явная транзакция ADO.NET: `DBHelper.Transaction` (`MySqlTransaction`).  
  Ссылка: `DBHelper.cs:594`.
- В SqlSugar есть helper-обертка `ExecuteTransaction`.  
  Ссылка: `Repository/SqlSugarHelper.cs:228`.

### 7.2 Что нетранзакционно (основная проблема)
- Критические multi-step операции (`insertStationProcode` + `updateProCode` + `SaveStationData` + `saveToFinalTable`) выполняются разрозненно без общей транзакции.  
  Ссылка: `MyTimer.cs:198`-`MyTimer.cs:236`.
- Логика `selectPrintCode` -> генерация -> `updatePrintCode` также без транзакции/lock.  
  Ссылка: `MyTimer.cs:210`, `MyTimer.cs:417`, `MyTimer.cs:475`.
- Большинство методов `DBHelper` открывают отдельное соединение на вызов, тем самым образуют автокоммитные границы.

### 7.3 Дополнительные нюансы
- `CreatNewTable` создает таблицы с `ENGINE=myisam`, что отключает транзакционность/FK на таких таблицах.  
  Ссылка: `DBHelper.cs:387`.
- В дампе основные таблицы `InnoDB`, но runtime может иметь смешанный engine-парк.

## 8) Integrity / security risks

| Риск | Наблюдение | Серьезность |
|---|---|---|
| SQL injection | Большой объем строковой конкатенации с внешними входами (`productCode`, `stationName`, `RFID`, UI text). Примеры: `DBHelper.cs:485`, `DBHelper.cs:511`, `DBHelper.cs:517`, `FrmReview.cs:336`, `FrmMain.cs:584`. | High |
| Schema drift | Код использует таблицы/колонки, которых нет в `lada_db.sql` (`printcode`, `RandomInnerId`, `OP80_2`, `op60_2`; `scanCode` vs `boxCode`; `stationprocode.RFID` в транзакции). | High |
| Секреты в коде | Логин/пароль БД в открытом виде (`root/123456`, плюс SQL Server `sa/123456`). | High |
| Отсутствие явных FK/UNIQUE | В дампе почти везде только PK по `ID`, нет FK между `finalprintcode` и `opXX`/`stationprocode`; высокий риск orphan/дубликатов. | High |
| Гонки на генерации print code | `selectPrintCode` + вычисление + `updatePrintCode` без lock/transaction -> возможны коллизии кодов при параллельной записи. | Medium |
| Слабая консистентность multi-write | Частичный успех шагов в `MyTimer` оставляет несогласованные данные между `stationprocode`, `finalprintcode`, станционными таблицами. | Medium |
| Разночтение имен/кейса | Используются `finalprintcode` и `finalprintCode`, `Station.name` в коде при `stationName` в дампе (`FrmStation.cs:30`, `FrmStation.cs:143`, `lada_db.sql:626`). | Medium |
| Лог SQL/параметров | `OnLogExecuting` пишет native SQL в консоль, что может утекать в логи runtime. | Low/Medium |

## 9) Data lineage по `productCode`

### 9.1 Семантика кодов
- `productCode`: основной идентификатор изделия (часто «код вала»).
- `barcode4` (из OP20): в финале хранится как `fixedEndCode`.
- `barcode3` (из OP30): в финале хранится как `innerCode`.
- `printCode`: печатный финальный код из `printcode`.
- `scanCode`: подтверждение сканированием финального print code (логически должен записываться в `finalprintcode`).

### 9.2 Линейдж записи
1. `productCode` приходит от станции в `StationData` (`StationData.cs:54`).
2. При входе изделия создается агрегат в `stationprocode` (`insertStationProcode`) и далее обновляется `productResult/stationName`.  
   Ссылки: `DBHelper.cs:554`, `DBHelper.cs:511`.
3. Сырые station-данные пишутся в таблицу станции (`SaveStationData` / `updateStationData*`).  
   Ссылки: `DBHelper.cs:629`, `DBHelper.cs:711`, `DBHelper.cs:755`, `DBHelper.cs:762`.
4. Для OP40 `saveToFinalTable` читает `op20` и `op30`, формирует `productCode + fixedEndCode + innerCode`, пишет строку в `finalprintcode`.  
   Ссылка: `DBHelper.cs:523`, `DBHelper.cs:533`.
5. После скан-подтверждения обновляется `finalprintcode.scanCode`, а `printcode.productCode` инкрементируется.  
   Ссылка: `DBHelper.cs:550`, `DBHelper.cs:929`.

### 9.3 Линейдж чтения в отчете
- `FrmReview.searchAllStation` начинает с `finalprintcode fin`.
- Основной join: `fin.productCode = OPxx.productCode`.
- Спец-join:
  - `OP100.productCode = fin.fixedEndCode`
  - `OP110.productCode = fin.innerCode`
- Ссылки: `FrmReview.cs:287`, `FrmReview.cs:294`, `FrmReview.cs:298`.

### 9.4 Линейдж fallback-резолвинга
- Если прямого совпадения `productCode` нет, код резолвит его через:
  - `OP30.barcode3 -> productCode`
  - `OP20.barcode4 -> productCode`
- Ссылки: `MyTimer.cs:138`, `MyTimer.cs:141`, `DBHelper.cs:485`, `Repository/SqlSugarHelper.cs:72`, `Repository/SqlSugarHelper.cs:77`.

## 10) Индекс ссылок (быстрый старт)
- Подключения: `App.config:8`, `App.config:9`, `Repository/DBcontext.cs:19`.
- Главный runtime-поток: `MyTimer.cs:98`, `MyTimer.cs:492`.
- DB helper: `DBHelper.cs:511`, `DBHelper.cs:517`, `DBHelper.cs:629`, `DBHelper.cs:771`.
- ORM helper: `Repository/SqlSugarHelper.cs:68`, `Repository/SqlSugarHelper.cs:228`.
- Отчеты: `FrmReview.cs:166`, `FrmReview.cs:256`, `FrmReviewData.cs:221`.
- Схема дампа: `lada_db.sql:24`, `lada_db.sql:546`, `lada_db.sql:571`, `lada_db.sql:610`, `lada_db.sql:636`.


