# 90. Onboarding Map

## Зачем этот документ
Быстрый вход в проект для инженера/агента: что это за система, где главные точки правды в коде и как безопасно начать изменения.

## 1) Что это за приложение
- Тип: WinForms-приложение на .NET Framework 4.8 (`LADA.csproj`).
- Домен: производственная трассируемость/контроль качества (линии OPxx, штрихкоды, RFID, печать).
- Точка входа: `Program.cs` -> `FrmLogIn` -> `FrmMain`.
- БД: MySQL (`App.config`, ключ `connectMySql`).

## 2) Быстрый старт (15-30 минут)
1. Установить окружение:
   - Visual Studio 2022 с workload ".NET desktop development".
   - Targeting pack .NET Framework 4.8.
   - MySQL Connector/NET 8.0.32 (в проекте есть hardcoded reference в `LADA.csproj`).
2. Поднять БД MySQL и проверить строку подключения в `App.config`.
3. Импортировать `lada_db.sql` как базовую схему.
4. Проверить дополнительные таблицы, которых нет в `lada_db.sql`, но они используются кодом:
   - `printcode`, `scancode`, `standardcode`, `tablegrade`, `codeverification`, `barcode`, `rs`, `operator`, `assistantstation`, `morestation`, `randominnerid`, `op60_2`.
5. Сборка:
   - Предпочтительно через Visual Studio (для legacy WinForms/.resx).
   - `dotnet msbuild` в текущем окружении падает с `MSB3822/MSB3823` (ресурсы `System.Resources.Extensions`).
6. Запуск:
   - Авторизация в `FrmLogIn` через таблицу `usertable`.
   - После входа основной сценарий в `FrmMain`.

## 3) Карта выполнения (runtime flow)
1. Инициализация:
   - `Program.cs` запускает `FrmLogIn`.
   - При успешном логине запускается `FrmMain`.
2. Старт производства (`FrmMain.btnStart_Click`):
   - Берется продукт/grade, optional code verification.
   - Читаются станции и сервер из таблицы `station`.
   - Создается `ProductLine` (TCP сервер для ПЛК/станций).
   - Создается/запускается `MyTimer` (циклическая обработка статусов и запись в БД).
3. Сетевой обмен:
   - Прием/отправка сокетов: `ProductLine.cs`.
   - Бинарный протокол маппится в/из модели: `MyConvert.cs`.
4. Основная бизнес-логика статусов:
   - `MyTimer.cs` (коды `status`/`feedBack`, трассируемость, RFID bind/unbind, сохранение станций, печать, final table update).

## 4) Где менять что
- Сетевая часть, TCP, прием/ответ станциям: `ProductLine.cs`, `MyConvert.cs`.
- Производственный цикл и коды статусов: `MyTimer.cs`.
- Экран старта, выбор продукта/grade, запуск линии: `FrmMain.cs`.
- Доступ к БД (legacy SQL helper): `DBHelper.cs`.
- ORM/typed доступ (SqlSugar): `Repository/DBcontext.cs`, `Repository/SqlSugarHelper.cs`, `Model/*.cs`.
- Пользователи/роли: `FrmUser.cs`, `FrmLogIn.cs`.
- Конфиг станций/маршрута: `FrmStation.cs`, `FrmProduct.cs`.
- Настройка полей трассировки: `FrmReviewData.cs`.
- Отчеты/поиск/экспорт: `FrmReview.cs`, `ExportDGVToExcel.cs`.
- Настройки штрихкодов/правил: `FrmBarcode.cs`, `UI/FrmStandCode.cs`, `UI/FrmVerifySCode.cs`, `UI/FormGradeNumber.cs`.
- Печать и шаблоны кода: `FrmSetPrintCode.cs`, `MyTimer.cs` (печать на Zebra).
- Логи: `config/log4net.config`, `Utils/LogFile.cs`, `Properties/AssemblyInfo.cs`.

## 5) БД: что считать source-of-truth
- Базовая схема: `lada_db.sql`.
- Реально используемые таблицы надо сверять с кодом (`DBHelper.cs`, `Model/*.cs`), т.к. `lada_db.sql` неполный.
- Каноничный список ORM-моделей:
  - `Model/printCode.cs`
  - `Model/scancode.cs`
  - `Model/standardcode.cs`
  - `Model/tablegrade.cs`
  - `Model/codeverification.cs`
  - `Model/stationprocode.cs`
  - `Model/product.cs`
  - `Model/stationModel.cs`

## 6) На что смотреть в первую очередь при инциденте
1. Нет связи со станциями:
   - `FrmMain.ReadIPAndPort()`, таблица `station`.
   - `ProductLine.startLisen()/AcceptSocketClient()/ReceiveData()`.
   - Проверка порта: `MyTools.isPortAvalaible`.
2. Не пишутся данные по станции:
   - `MyTimer.DealNetUsedData()/DealNetDownLineUsedData()`.
   - `DBHelper.SaveStationData()/updateStationData()`.
3. Проблемы код-верификации:
   - `FrmMain.CodeVerification()`.
   - `UI/FrmStandCode.cs`, `UI/FrmVerifySCode.cs`, `UI/FormGradeNumber.cs`.
4. Проблемы печати:
   - `FrmSetPrintCode.cs` и `MyTimer.printCode()`.
   - IP принтера сейчас захардкожен (`192.168.1.45`).

## 7) Известные риски/долг
- Неполная SQL-схема в `lada_db.sql` относительно фактических таблиц кода.
- Много SQL строк через конкатенацию в `DBHelper.cs` и формах.
- Case mismatch для server-станции (`server`/`Server`) в разных местах.
- Hardcoded значения:
  - путь к `MySql.Data.dll` в `LADA.csproj`,
  - IP принтера в `FrmMain.cs` и `MyTimer.cs`,
  - путь `D:\\Marking Lable Template\\11.prn`.
- Нет автотестов; основная проверка сейчас ручная через UI/линейный сценарий.

## 8) Финальный набор docs (на текущий момент)
- `docs/00-system-overview.md`
- `docs/10-plc-communication.md`
- `docs/20-database-model-and-flows.md`
- `docs/30-zebra-printing.md`
- `docs/35-ui-architecture-and-responsibilities.md`
- `docs/40-end-to-end-runtime-flow.md`
- `docs/50-operations-runbook.md`
- `docs/60-error-catalog-and-troubleshooting.md`
- `docs/70-security-baseline.md`
- `docs/80-localization-and-ui-language.md`
- `docs/90-onboarding-map.md`
- `docs/95-agents-operational-index.md`

При добавлении новых документов обновляй индекс в `docs/95-agents-operational-index.md`.



