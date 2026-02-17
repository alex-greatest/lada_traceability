# Security Baseline (As-Is) and Prioritized Actions

## 1. Назначение
Документ фиксирует текущую security-картину системы `Review` и минимальные operational меры до полного рефакторинга.

Важно: документ описывает состояние `as-is` и не заменяет полноценную secure-architecture программу.

## 2. Активы и доверенные границы
Критичные активы:
1. Производственные данные (`opXX`, `stationprocode`, `finalprintcode`).
2. Учетные данные пользователей (`usertable`).
3. Конфигурация и учетные данные подключения к БД.
4. Команды/статусы PLC и печать.

Границы:
- между UI и DB (`DBHelper`, `SqlSugarHelper`)
- между сервером приложения и станциями (`ProductLine` TCP)
- между приложением и принтером (`Zebra TCP`)

## 3. Текущие ключевые риски

### R1 (P0): SQL injection в data layer
Наблюдение:
- Значимая часть SQL в `DBHelper` строится через string concatenation.

Критичные точки:
- `DBHelper.updateStationData_20` (`DBHelper.cs:755`)
- `DBHelper.updateStationData_30` (`DBHelper.cs:762`)
- `DBHelper.RFIDBind` (`DBHelper.cs:771`)
- `DBHelper.clearRFIDBind` (`DBHelper.cs:785`)
- `DBHelper.SelectRFIDBindingCode` (`DBHelper.cs:795`)

Риск:
- модификация/удаление данных, чтение неразрешенных данных, подмена технологических результатов.

Минимальная мера:
- запретить новые SQL-конкатенации; для новых изменений обязательны параметризованные запросы.

### R2 (P0): Plaintext credentials в конфигурации
Наблюдение:
- Connection strings с паролями в `App.config`.

Кодовые точки:
- `App.config:8`
- `App.config:9`

Риск:
- компрометация БД при утечке файла/дистрибутива.

Минимальная мера:
- контролируемый доступ к конфигам и бинарям, отдельные учетки для окружений, ротация паролей.

### R3 (P0): Пароли пользователей без хеширования
Наблюдение:
- Login сравнивает `userPwd` как plaintext.

Кодовые точки:
- `DBHelper.UserlogIn` (`DBHelper.cs:349`)
- SQL: `select * from userTable where userName=@name and userPwd=@pwd` (`DBHelper.cs:352`)

Риск:
- полный компромисс аккаунтов при утечке БД.

Минимальная мера:
- до миграции: ограничить доступ к таблице `usertable` и резервным копиям.

### R4 (P1): Слабая авторизация на уровне UI
Наблюдение:
- Ограничение в основном на уровне открытия форм (`ShowWindow`), не централизованная policy-проверка на каждую операцию.

Кодовые точки:
- `FrmMain.ShowWindow` (`FrmMain.cs:372`)

Риск:
- обход ограничений через нештатные пути вызова.

Минимальная мера:
- административные операции только под отдельной процедурой доступа и аудитом.

### R5 (P1): Неаутентифицированный TCP канал PLC
Наблюдение:
- plain TCP для станций без криптографической аутентификации.

Кодовые точки:
- `ProductLine.startLisen` (`ProductLine.cs:122`)
- `ProductLine.ReceiveData` (`ProductLine.cs:215`)
- `ProductLine.SendData` (`ProductLine.cs:253`)

Риск:
- spoofing станции, подмена трафика в локальной сети.

Минимальная мера:
- изоляция сети, firewall ACL по IP/port, сегментация VLAN.

### R6 (P1): Hardcoded printer endpoint
Наблюдение:
- IP принтера захардкожен в runtime.

Кодовые точки:
- `MyTimer.cs:668`
- `FrmMain.cs:722`

Риск:
- операционная хрупкость, риск несанкционированной смены сетевого контура.

Минимальная мера:
- change-control для printer network settings.

## 4. Приоритетная матрица действий

### P0 (немедленно)
1. Ввести правило: новые/измененные SQL только parameterized.
2. Ограничить доступ к `App.config` и артефактам с секретами.
3. Ограничить доступ к `usertable` и бэкапам БД.
4. Для всех критичных инцидентов фиксировать forensic-лог в `docs/60-error-catalog-and-troubleshooting.md`.

### P1 (краткосрочно)
1. Ввести сетевую сегментацию и ACL для PLC и принтера.
2. Централизовать проверку прав для административных операций.
3. Убрать hardcoded endpoints в конфигурацию с контролем изменений.

### P2 (среднесрочно)
1. Миграция authentication на hashed passwords (Argon2/BCrypt + salt).
2. Укрепление транспортной модели PLC (подпись/аутентификация сообщений).
3. Стандартизация слоя доступа к БД (снижение доли legacy raw SQL).

## 5. Security operational checklist
Перед выпуском/изменением проверять:
1. Нет новых SQL-конкатенаций с пользовательскими или внешними данными.
2. Нет новых секретов в коде/конфигах.
3. Не изменен протокол PLC без обновления `docs/10-plc-communication.md`.
4. Не изменена логика печати без обновления `docs/30-zebra-printing.md`.
5. Для новых ошибок добавлены записи в `docs/60-error-catalog-and-troubleshooting.md`.

## 6. Связанные документы
- `docs/10-plc-communication.md`
- `docs/20-database-model-and-flows.md`
- `docs/30-zebra-printing.md`
- `docs/50-operations-runbook.md`
- `docs/60-error-catalog-and-troubleshooting.md`
- `AGENTS.md`


