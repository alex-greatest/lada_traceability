# LADA Review Documentation Hub

Короткий навигатор по документации проекта `Review` (WinForms, production traceability).

## Что это
Система управляет контуром:
`PLC/Stations -> Review runtime -> MySQL -> Zebra printing`

Основная точка входа в код: `Program.cs -> FrmLogIn -> FrmMain`.

## С чего начать (5 минут)
1. Общая картина: `docs/00-system-overview.md`
2. Сквозной runtime-поток: `docs/40-end-to-end-runtime-flow.md`
3. Индекс source-of-truth: `docs/95-agents-operational-index.md`

## Сборка
Стандартная команда для репозитория:

```powershell
.\build.ps1 -Configuration Debug
```

Дополнительно:

```powershell
.\build.ps1 -Configuration Release -Clean
.\build.ps1 -Restore
```

Скрипт автоматически находит `MSBuild.exe` через `vswhere` и использует Visual Studio toolchain, что корректно для этого legacy WinForms-проекта.  
`dotnet msbuild` не используется как стандарт из-за возможных проблем с `.resx`-ресурсами (`MSB3822/MSB3823`).

## Документы по направлениям
- PLC и протокол: `docs/10-plc-communication.md`
- БД и потоки данных: `docs/20-database-model-and-flows.md`
- Печать Zebra: `docs/30-zebra-printing.md`
- UI: роли и ответственность форм: `docs/35-ui-architecture-and-responsibilities.md`
- Эксплуатация (runbook): `docs/50-operations-runbook.md`
- Ошибки и recovery: `docs/60-error-catalog-and-troubleshooting.md`
- Security baseline: `docs/70-security-baseline.md`
- Локализация UI: `docs/80-localization-and-ui-language.md`
- Онбординг: `docs/90-onboarding-map.md`

## Для агентов и правил изменений
- Политики и обязательный порядок чтения: `AGENTS.md`
- Быстрый индекс вопросов/источников: `docs/95-agents-operational-index.md`

## Принцип актуальности
Если меняется поведение PLC/DB/Print/UI, обновляй соответствующий документ в `docs/` в том же пакете изменений.

