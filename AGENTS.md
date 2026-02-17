# AGENTS.md

## Mission
Этот репозиторий содержит производственный WinForms-клиент `Review` для контура:
`PLC/Stations -> Review runtime -> MySQL -> Zebra printing`.

Главный приоритет любого агента: не нарушить целостность операционного контура и трассируемость данных.

## Scope and Critical Path
Критичный путь:
1. login и запуск линии
2. прием/обработка станционных пакетов
3. запись результатов в БД
4. печать маркировки
5. ревью/экспорт данных

Любые изменения в этих частях считаются high-risk.

## Mandatory Read Order (Before Any Change)
Перед изменениями агент обязан прочитать документы в порядке:
1. `docs/00-system-overview.md`
2. `docs/40-end-to-end-runtime-flow.md`
3. профильный документ по изменению:
- PLC: `docs/10-plc-communication.md`
- DB: `docs/20-database-model-and-flows.md`
- Print: `docs/30-zebra-printing.md`
- UI: `docs/35-ui-architecture-and-responsibilities.md`
4. `docs/50-operations-runbook.md`
5. `docs/60-error-catalog-and-troubleshooting.md`
6. `docs/70-security-baseline.md`
7. `docs/80-localization-and-ui-language.md` (если затронут UI текст/культура)
8. `docs/90-onboarding-map.md`
9. `docs/95-agents-operational-index.md`

## Hard Safety Rules
1. Запрещено менять протокол PLC (`ProductLine`/`MyConvert`/`StationData`/`MasterData`) без обновления `docs/10-plc-communication.md` в том же PR/пакете изменений.
2. Запрещено менять SQL-модели/потоки записи без обновления `docs/20-database-model-and-flows.md`.
3. Запрещено менять триггеры/формат/транспорт печати без обновления `docs/30-zebra-printing.md`.
4. Запрещено вносить новые plain secrets в код/конфиги.
5. Принятое исключение по secret-политике: в `App.config` допускаются plaintext credentials только для ключей `connectMySql_prod` и `connectMySql_test` (scope ограничен этим репозиторием и этими ключами).
6. Исключение из п.5 является постоянным (без срока действия) и применяется без дополнительных compensating controls.
7. Запрещено добавлять новые SQL-конкатенации с внешними данными; использовать параметризованные запросы или безопасный ORM-путь.
8. Любой новый production-инцидент обязан быть добавлен в `docs/60-error-catalog-and-troubleshooting.md`.

## Change Workflow
1. Определи затронутый контур: `PLC`, `DB`, `Print`, `UI`, `Ops`.
2. Сверься с source-of-truth документом из индекса.
3. Внеси изменение.
4. Обнови соответствующий документ(ы) в `docs/`.
5. Добавь impact note:
- что изменено
- что может сломаться
- как откатить
6. Обнови troubleshooting/runbook, если поведение в эксплуатации изменилось.

## Verification Checklist (Minimum)
Перед завершением работы агент обязан проверить:
1. Line start/stop логика не регрессировала (`FrmMain`, `MyTimer`).
2. PLC обмен не сломан (подключение, decode/encode, feedback).
3. DB записи остаются консистентными по ключевым таблицам.
4. Print pipeline работает (trigger -> template -> send).
5. Ошибки и восстановление задокументированы.
6. Если менялся UI-текст/локализация, обновлен `docs/80-localization-and-ui-language.md`.

## Incident Documentation Rule
Если в ходе работы найден новый сбой:
1. присвоить ID (`E-<DOMAIN>-<NNN>`)
2. описать symptoms, likely causes, verification, recovery
3. добавить в `docs/60-error-catalog-and-troubleshooting.md`

Без этого задача считается неполной.

## Source of Truth by Topic
- Архитектура и границы: `docs/00-system-overview.md`
- PLC протокол/канал: `docs/10-plc-communication.md`
- DB схема и потоки: `docs/20-database-model-and-flows.md`
- Печать Zebra: `docs/30-zebra-printing.md`
- UI роли и навигация: `docs/35-ui-architecture-and-responsibilities.md`
- Сквозной runtime: `docs/40-end-to-end-runtime-flow.md`
- Эксплуатация: `docs/50-operations-runbook.md`
- Ошибки и восстановление: `docs/60-error-catalog-and-troubleshooting.md`
- Security baseline: `docs/70-security-baseline.md`
- Локализация: `docs/80-localization-and-ui-language.md`
- Онбординг: `docs/90-onboarding-map.md`
- Индекс для агентов: `docs/95-agents-operational-index.md`

## Ownership and Editing Constraints for Agents
1. Если задачи разделены между агентами, каждый агент редактирует только назначенные файлы.
2. Не перезаписывать изменения других агентов.
3. При конфликте источников правды приоритет у документов `docs/` (после их обновления в рамках текущего пакета).

## Done Criteria for Agent Tasks
Задача считается завершенной только если:
1. Изменения в коде (если были) и соответствующие изменения в документации синхронизированы.
2. Пройдены минимальные проверки из checklist.
3. Добавлены/обновлены шаги troubleshooting при изменении operational поведения.
4. Обновлены ссылки/индекс, если добавлен новый документ.


