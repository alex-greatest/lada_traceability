# 95. Agents Operational Index (Source-of-Truth)

## Назначение
Этот индекс отвечает на вопрос: "Где правда по конкретной теме?"  
Формат: `вопрос -> первичный документ -> резервный источник`.

## Финальный набор docs (текущий)
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
- `docs/LADA_Review_Руководство.md`

## Вопрос -> документ

| Вопрос | Primary source-of-truth | Backup source |
|---|---|---|
| Что делает система на уровне архитектуры? | `docs/00-system-overview.md` | `Program.cs`, `FrmMain.cs` |
| Где детально описан PLC/TCP протокол и статусы? | `docs/10-plc-communication.md` | `ProductLine.cs`, `MyTimer.cs`, `MyConvert.cs` |
| Где детально описаны модель БД и потоки данных? | `docs/20-database-model-and-flows.md` | `DBHelper.cs`, `Repository/SqlSugarHelper.cs`, `lada_db.sql` |
| Где детально описана печать Zebra и ее триггеры? | `docs/30-zebra-printing.md` | `MyTimer.cs`, `FrmSetPrintCode.cs` |
| Где описана UI-архитектура и ответственность форм? | `docs/35-ui-architecture-and-responsibilities.md` | `FrmMain.cs`, `Frm*.cs` |
| Где описан end-to-end runtime flow? | `docs/40-end-to-end-runtime-flow.md` | `Program.cs`, `FrmMain.cs`, `MyTimer.cs` |
| Где операционные процедуры запуска/остановки и recovery? | `docs/50-operations-runbook.md` | `FrmMain.cs`, `ProductLine.cs`, `MyTimer.cs` |
| Где каталог типовых инцидентов и recovery? | `docs/60-error-catalog-and-troubleshooting.md` | `config/log4net.config`, `MyTimer.cs` |
| Где security baseline и приоритеты hardening? | `docs/70-security-baseline.md` | `App.config`, `DBHelper.cs` |
| Где описаны локализация и языковое поведение UI? | `docs/80-localization-and-ui-language.md` | `Resources/Strings*.resx`, `FrmMain.cs` |
| Где onboarding по проекту? | `docs/90-onboarding-map.md` | `docs/95-agents-operational-index.md` |
| Где пользовательское руководство по эксплуатации UI/процесса? | `docs/LADA_Review_Руководство.md` | `docs/50-operations-runbook.md`, `docs/90-onboarding-map.md` |
| Где навигационный индекс source-of-truth? | `docs/95-agents-operational-index.md` | `docs/00-system-overview.md` |
| Где точка входа приложения? | `Program.cs` | `LADA.sln` |
| Какой основной пользовательский сценарий после логина? | `FrmMain.cs` | `FrmLogIn.cs` |
| Где старт/стоп производства? | `FrmMain.cs` (`btnStart_Click`, `btnStop_Click`) | `MyTimer.cs` |
| Где логика TCP-обмена со станциями? | `ProductLine.cs` | `MyConvert.cs` |
| Как маппится бинарный пакет в поля станции/мастера? | `MyConvert.cs` | `StationData.cs`, `MasterData.cs` |
| Где логика status/feedBack кодов станций? | `MyTimer.cs` | `MasterData.cs` |
| Где создается конфигурация маршрута (используемые/неиспользуемые станции)? | `FrmMain.cs` (`ConfigIPAndPort`, `TechnologyDataConfig`) | `ProductLine.cs` |
| Где запись данных станций в БД? | `DBHelper.cs` (`SaveStationData`, `updateStationData`) | `Repository/SqlSugarHelper.cs` |
| Где логика RFID bind/unbind? | `MyTimer.cs` | `DBHelper.cs` (`RFIDBind`, `SelectRFIDBindingCode`, `clearRFIDBind`) |
| Где логика итогового штрихкода и final table? | `MyTimer.cs` | `DBHelper.cs` (`saveToFinalTable`, `updateScanCode`) |
| Где конфиг печати и генерации productCode? | `FrmSetPrintCode.cs` | `Utils/Utils.cs` (`GenerateNextCode`) |
| Где фактическая отправка задания на Zebra? | `MyTimer.cs` (`printCode`) | `FrmMain.cs` (`printCode`) |
| Где настройки COM порта? | `FrmRS.cs` | `FrmBarcode.cs` (`IniSerialPort`) |
| Где управление пользователями и ролями? | `FrmUser.cs` | `FrmLogIn.cs`, `DBHelper.cs` (`UserlogIn`) |
| Где управление станциями (IP/порт)? | `FrmStation.cs` | `FrmMain.cs` (`ReadIPAndPort`) |
| Где управление маршрутами продукта (station1..station14)? | `FrmProduct.cs` | `DBHelper.cs` (`selectStationList`) |
| Где настраивается словарь полей для review/поиска? | `FrmReviewData.cs` (`reviewdata`) | `FrmReview.cs` |
| Где выполняется поисковая трассировка и экспорт? | `FrmReview.cs` | `ExportDGVToExcel.cs` |
| Где хранятся/редактируются стандартные коды деталей? | `UI/FrmStandCode.cs` | `Model/standardcode.cs` |
| Где хранятся/редактируются скан-коды партий? | `UI/FrmVerifySCode.cs` | `Model/scancode.cs` |
| Где настраиваются grade и правила codeverification? | `UI/FormGradeNumber.cs` | `Model/tablegrade.cs`, `Model/codeverification.cs` |
| Где подключение к БД и ORM-конфиг? | `App.config` | `Repository/DBcontext.cs` |
| Где глобальные SQL/ORM helper-операции? | `DBHelper.cs` | `Repository/SqlSugarHelper.cs` |
| Где настройки логирования? | `config/log4net.config` | `Properties/AssemblyInfo.cs`, `Utils/LogFile.cs` |
| Где список NuGet-зависимостей? | `packages.config` | `LADA.csproj` |
| Какая SQL-схема в репозитории? | `lada_db.sql` | `Model/*.cs` и SQL из `DBHelper.cs` |
| Почему схема может не совпадать с кодом? | `docs/90-onboarding-map.md` (раздел БД/риски) | `DBHelper.cs`, `Model/*.cs` |
| Где экран мониторинга онлайновых данных станций? | `FrmNet.cs` | `FrmMain.cs` (`MenuItemNet_Click`) |
| Где экран производственного табло? | `FrmDisplay.cs` | `Utils/AppContext.cs` |
| Где legacy-настройки assistant/more stations? | `FrmAssistantStation.cs`, `moreStation.cs` | `DBHelper.cs` (`assistantSaveToMain`, `assistantUpdateToMain`) |

## Оперативная памятка для агента
1. Если вопрос про бизнес-логику сигнала от станции: начинай с `MyTimer.cs`.
2. Если вопрос про "почему в БД не так": сверяй `DBHelper.cs` + `Model/*.cs`, не только `lada_db.sql`.
3. Если вопрос про UI-поведение: сначала форма `Frm*.cs`, потом связанный helper/модель.
4. Если вопрос про строку/перевод: `Resources/Strings.resx` и `Resources/Strings.ru.resx`.

## Как обновлять индекс
1. Добавь новую строку в таблицу "Вопрос -> документ".
2. Если появился новый документ в `docs/`, дополни раздел "Финальный набор docs".
3. Если source-of-truth сменился (рефакторинг), обнови оба столбца `Primary` и `Backup`.
4. Перед фиксацией проверь фактический набор: `Get-ChildItem docs -Name`.


