# Локализация и язык UI (ru-RU / zh-CN)

## 1. Поддерживаемые языки и фактическая модель ресурсов

Система работает в двух целевых языках:

- `ru-RU` (русский);
- `zh-CN` (китайский, через нейтральный ресурс).

Ключевые файлы:

- Базовые строки: [`Resources/Strings.resx`](../Resources/Strings.resx) (70 ключей).
- Русские строки: [`Resources/Strings.ru.resx`](../Resources/Strings.ru.resx) (70 ключей).
- Генератор ресурсов: [`Resources/Strings.Designer.cs`](../Resources/Strings.Designer.cs).
- Конфигурация в проекте: [`LADA.csproj`](../LADA.csproj).

Важно:

- отдельного `Strings.zh-CN.resx` нет; `zh-CN` фактически берет строки из `Strings.resx`;
- `Strings.ru.resx` полностью покрывает ключи `Strings.resx` (на текущем срезе ключи совпадают 1:1).

## 2. Механизм переключения языка

Точка переключения: [`FrmMain.buttonChangeLanguage_Click`](../FrmMain.cs).

Поток:

1. В [`FrmMain`](../FrmMain.cs) создается `ResourceManager("Review.Resources.Strings", ...)`.
2. При старте выставляется `currentCulture = new CultureInfo("ru-RU")`.
3. Нажатие `Переключить язык` меняет культуру между `ru-RU` и `zh-CN`.
4. Вызывается [`FrmMain.UpdateLanguage`](../FrmMain.cs), где:
   - проставляются `Thread.CurrentThread.CurrentCulture` и `CurrentUICulture`;
   - обновляются caption/menu/labels через `resManager.GetString(...)`;
   - локализуются вспомогательные сообщения `s1..s13` для текущей формы.

Связка с дочерними окнами:

- локализуемые формы получают `ResourceManager` и `CultureInfo` через конструктор из [`FrmMain`](../FrmMain.cs), например:
  - [`new FrmUser(this.resManager, this.currentCulture)`](../FrmMain.cs),
  - [`new FrmReview(this.resManager, this.currentCulture)`](../FrmMain.cs),
  - [`new FrmStandCode(this.resManager, this.currentCulture)`](../FrmMain.cs).

## 3. Coverage локализации (что покрыто, что нет)

### 3.1 Формы с явной runtime-локализацией (`UpdateLanguage`)

| Форма | Файл | Локализация через ресурсы | Доп. ручные строки |
|---|---|---|---|
| Главная форма | [`FrmMain`](../FrmMain.cs) | Да (`resManager.GetString`) | Да (`s1..s13`) |
| Пользователи | [`FrmUser`](../FrmUser.cs) | Да | Да (`s1..s9`) |
| Продукты | [`FrmProduct`](../FrmProduct.cs) | Да | Да (`s1..s12`) |
| Отслеживание | [`FrmReview`](../FrmReview.cs) | Да | Да (`s1..s7`) |
| Настройка ReviewData | [`FrmReviewData`](../FrmReviewData.cs) | Да | Да (`s1..s2`) |
| Настройка print-code | [`FrmSetPrintCode`](../FrmSetPrintCode.cs) | Да | Частично (`s1`, `OK/NOK`) |
| Настройка grade | [`FormGradeNumber`](../UI/FormGradeNumber.cs) | Частично (только tab captions) | Остальное hardcoded |
| Эталонные коды | [`FrmStandCode`](../UI/FrmStandCode.cs) | Да | Да (`s1..s4`) |
| Проверка скан-кодов | [`FrmVerifySCode`](../UI/FrmVerifySCode.cs) | Да | Да (`s1..s4`) |

### 3.2 Формы без полноценной runtime-локализации

| Форма | Файл | Текущее состояние |
|---|---|---|
| Логин | [`FrmLogIn`](../FrmLogIn.cs) | hardcoded строки, ресурсы не используются |
| Эскалация прав | [`FrmLog`](../FrmLog.cs) | hardcoded строки |
| Сетевой монитор | [`FrmNet`](../FrmNet.cs) | hardcoded подписи |
| Станции | [`FrmStation`](../FrmStation.cs) | hardcoded сообщения |
| RS-параметры | [`FrmRS`](../FrmRS.cs) | hardcoded сообщения |
| Штрихкоды (старый UI) | [`FrmBarcode`](../FrmBarcode.cs) | hardcoded сообщения и подписи |
| Группы/операторы | [`FrmGroup`](../FrmGroup.cs) | hardcoded сообщения |
| Производственная доска | [`FrmDisplay`](../FrmDisplay.cs) | нет ресурсной локализации; дата принудительно через `ru-RU` |

## 4. Hardcoded gaps (ключевые разрывы)

### 4.1 Текст вне `Resources/Strings*.resx`

- Сообщения об ошибках/подсказках зашиты в код:
  - startup fail-fast для профиля БД (`Program.Main`) использует `MessageBox` с техническим текстом без `Resources`;
  - [`FrmReviewData.CheckAllTextBoxesEmpty`](../FrmReviewData.cs),
  - [`FrmStation`](../FrmStation.cs),
  - [`FrmRS`](../FrmRS.cs),
  - [`FrmBarcode`](../FrmBarcode.cs),
  - [`ExportDGVToExcel`](../ExportDGVToExcel.cs),
  - [`FrmLogIn`](../FrmLogIn.cs), [`FrmLog`](../FrmLog.cs).
- Пункт меню `档次号设置` задан в дизайнере напрямую: [`FrmMain.Designer`](../FrmMain.Designer.cs) (без `resManager.GetString`).

### 4.2 Локализация покрывает не все контролы/ветки

- В [`FormGradeNumber.UpdateLanguage`](../UI/FormGradeNumber.cs) меняются только заголовки вкладок.
- В [`FrmDisplay.UpdateDateTimeLabels`](../FrmDisplay.cs) культура даты жестко `ru-RU`.
- Открытые дочерние окна не переинициализируются при смене языка автоматически (переключение идет только через `FrmMain.UpdateLanguage`).

### 4.3 Качество переводов и ресурсных ключей

- В [`Resources/Strings.ru.resx`](../Resources/Strings.ru.resx) значение ключа `alwaysVerification` сейчас: `Имя пользователя` (семантически не соответствует чекбоксу).
- В проекте включен [`Resources/Strings.ru.Designer.cs`](../Resources/Strings.ru.Designer.cs), но файл пустой; фактически используется общий [`Strings.Designer.cs`](../Resources/Strings.Designer.cs).

## 5. Эксплуатационные рекомендации

### 5.1 Для операторской эксплуатации

1. Перед сменой проверять язык через `Система -> Переключить язык` на главной форме.
2. После смены языка закрывать и переоткрывать вторичные формы, которые уже были открыты до переключения.
3. Для форм со сканером (`FrmStandCode`, `FrmVerifySCode`, `FrmBarcode`) учитывать, что часть ошибок COM-порта hardcoded и может остаться на другом языке.

### 5.2 Для сопровождения (release/поддержка)

1. Централизовать все UI-строки в `Resources/Strings*.resx` и убрать hardcoded `MessageBox`-тексты.
2. Добавить `Resources/Strings.zh-CN.resx` для явного управления китайской локалью.
3. Добавить единый `LanguageService`/событие смены культуры для обновления уже открытых форм.
4. Исправить проблемные переводы (`alwaysVerification`) и ввести чек-лист ревью переводов перед релизом.
5. Сохранение выбранной культуры между запусками (например, в `App.config`) вынести в обязательный UX-требование.

## 6. Ссылки на ключевые точки реализации

- Инициализация и переключение: [`FrmMain`](../FrmMain.cs), [`FrmMain.Designer`](../FrmMain.Designer.cs)
- Startup DB profile preflight (не локализован): [`Program`](../Program.cs), [`Utils/DbProfileResolver`](../Utils/DbProfileResolver.cs)
- Ресурсы: [`Resources/Strings.resx`](../Resources/Strings.resx), [`Resources/Strings.ru.resx`](../Resources/Strings.ru.resx), [`Resources/Strings.Designer.cs`](../Resources/Strings.Designer.cs)
- Локализуемые формы: [`FrmUser`](../FrmUser.cs), [`FrmProduct`](../FrmProduct.cs), [`FrmReview`](../FrmReview.cs), [`FrmReviewData`](../FrmReviewData.cs), [`FrmSetPrintCode`](../FrmSetPrintCode.cs), [`UI/FrmStandCode`](../UI/FrmStandCode.cs), [`UI/FrmVerifySCode`](../UI/FrmVerifySCode.cs), [`UI/FormGradeNumber`](../UI/FormGradeNumber.cs)
- Нелокализованные/частично локализованные формы: [`FrmLogIn`](../FrmLogIn.cs), [`FrmLog`](../FrmLog.cs), [`FrmNet`](../FrmNet.cs), [`FrmStation`](../FrmStation.cs), [`FrmRS`](../FrmRS.cs), [`FrmBarcode`](../FrmBarcode.cs), [`FrmDisplay`](../FrmDisplay.cs)


