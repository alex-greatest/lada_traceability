# LADA Review System: System Overview

## 1. Цель системы
Система `Review` — это WinForms-приложение для управления и трассировки производственного процесса: прием статусов от станций/PLC, валидация кодов, запись результатов в MySQL, печать этикеток Zebra и просмотр истории/отчетов.

Ключевой operational контур:
- `UI (FrmMain/Frm* forms)`
- `TCP exchange with stations/PLC` via `ProductLine`
- `Business timer loop` via `MyTimer`
- `Persistence` via `DBHelper` + `SqlSugarHelper`
- `Printing` via Zebra SDK

## 2. Границы и контекст
Внутри приложения:
- авторизация пользователя и разграничение экранов по уровню (`FrmLogIn`, `FrmMain.ShowWindow`)
- запуск/останов линии
- обработка входящих пакетов станций и формирование feedback
- запись технологических данных и результатов
- печать этикеток
- просмотр и экспорт данных ревью

Внешние зависимости:
- MySQL (`lada_db`)
- PLC/станции по TCP
- Zebra принтер по TCP:9100
- локальный HTTP endpoint для аудио (`HttpToSound`)

## 3. Технологический профиль
- Platform: `.NET Framework 4.8` (`LADA.csproj`)
- UI: `WinForms`
- Data: `MySql.Data` + `SqlSugar`
- Logging: `log4net` (`config/log4net.config`)
- Printing SDK: `Zebra.Printer.Card.SDK`

Связанные документы:
- PLC: `docs/10-plc-communication.md`
- DB: `docs/20-database-model-and-flows.md`
- Printing: `docs/30-zebra-printing.md`
- Runtime flow: `docs/40-end-to-end-runtime-flow.md`

## 4. Точки входа и жизненный цикл
### 4.1 Startup
- `Program.Main` создает `FrmLogIn` и запускает `FrmMain` только при успешном `LoginFlag`.
- Кодовые точки: `Program.cs:15`, `Program.cs:19`, `Program.cs:23`.

### 4.2 Login
- `FrmLogIn` вызывает `DBHelper.UserlogIn` для проверки `userTable`.
- Кодовые точки: `FrmLogIn.cs`, `DBHelper.cs:349`.

### 4.3 Main control loop
- `FrmMain.btnStart_Click` выполняет валидацию, `CodeVerification`, `TechnologyDataConfig`, стартует runtime-процесс.
- Кодовые точки: `FrmMain.cs:507`, `FrmMain.cs:529`, `FrmMain.cs:551`, `FrmMain.cs:360`.

### 4.4 Network + timer orchestration
- `ProductLine` слушает серверный порт, принимает сокеты станций и обновляет `StationData/MasterData`.
- `MyTimer` с периодом 200ms обрабатывает `DealNetUsedData` и `DealNetDownLineUsedData`.
- Кодовые точки: `ProductLine.cs:122`, `ProductLine.cs:215`, `MyTimer.cs:69`, `MyTimer.cs:92`, `MyTimer.cs:98`, `MyTimer.cs:492`.

### 4.5 Printing
- Печать вызывается из runtime-потока и использует Zebra TCP (`TcpConnection.DEFAULT_ZPL_TCP_PORT`).
- Кодовые точки: `MyTimer.cs:667`, `MyTimer.cs:703`, `MyTimer.cs:706`, `FrmMain.cs:720`.

## 5. Архитектурные компоненты (as-is)
### 5.1 UI уровень
Основные формы:
- `FrmMain`: центр управления линией
- `FrmNet`: сетевой мониторинг станций
- `FrmReview`/`FrmReviewData`: ревью и анализ данных
- `FrmProduct`, `FrmStation`, `FrmUser`: справочники/администрирование
- `FrmDisplay`: производственный экран/индикаторы

Подробно: `docs/35-ui-architecture-and-responsibilities.md`.

### 5.2 Data access уровень
- `DBHelper`: legacy raw SQL слой (много ad-hoc методов)
- `SqlSugarHelper`: ORM-обертка для части операций
- `DBcontext`: конфиг `SqlSugarScope`

Подробно: `docs/20-database-model-and-flows.md`.

### 5.3 Device/Protocol уровень
- `ProductLine`: TCP server + session handling
- `MyConvert`: преобразование бинарных пакетов
- `StationData`/`MasterData`: runtime contract

Подробно: `docs/10-plc-communication.md`.

### 5.4 Printing уровень
- Шаблоны `printcode` + `.prn`
- Подстановка полей и отправка в Zebra

Подробно: `docs/30-zebra-printing.md`.

## 6. Data domains (ключевые таблицы)
- `station`, `stationprocode`, `opXX` (станционные данные)
- `finalprintcode` (итог по изделию/кодам)
- `reviewdata` (конфиг ревью-представления)
- `rfidbind` (привязка RFID)
- `usertable` (пользователи)

Кодовые точки схемы: `lada_db.sql:24`, `lada_db.sql:336`, `lada_db.sql:571`, `lada_db.sql:610`, `lada_db.sql:624`, `lada_db.sql:636`, `lada_db.sql:651`.

## 7. Операционные риски верхнего уровня
- SQL concatenation в `DBHelper`.
- Plaintext credentials в `App.config`.
- Частичная обработка ошибок и локальные hardcoded параметры (например IP принтера).
- Отсутствие единообразной транзакционности.

Подробно:
- `docs/60-error-catalog-and-troubleshooting.md`
- `docs/70-security-baseline.md`

## 8. Source-of-truth карта
- Архитектура/границы: этот документ
- PLC протокол: `docs/10-plc-communication.md`
- БД и потоки данных: `docs/20-database-model-and-flows.md`
- Печать: `docs/30-zebra-printing.md`
- Runtime последовательность: `docs/40-end-to-end-runtime-flow.md`
- Эксплуатация: `docs/50-operations-runbook.md`
- Инциденты: `docs/60-error-catalog-and-troubleshooting.md`
- Безопасность: `docs/70-security-baseline.md`
- Локализация UI: `docs/80-localization-and-ui-language.md`
- Онбординг: `docs/90-onboarding-map.md`
- Индекс для агентов: `docs/95-agents-operational-index.md`


