# UI-архитектура и зоны ответственности

## 1. Контур UI и точка входа

Приложение реализовано как WinForms-клиент с единой главной формой-оркестратором.

- Точка входа: [`Program.Main`](../Program.cs)  
  Поток запуска: `FrmLogIn` (`ShowDialog`) -> при успешном логине `Application.Run(new FrmMain(user, level))`.
- Главный оркестратор UI: [`FrmMain`](../FrmMain.cs)  
  Здесь сосредоточены:
  - инициализация языка/ресурсов ([`FrmMain.UpdateLanguage`](../FrmMain.cs));
  - запуск/останов производственного цикла ([`FrmMain.btnStart_Click`](../FrmMain.cs), [`FrmMain.btnStop_Click`](../FrmMain.cs));
  - открытие вторичных форм ([`FrmMain.ShowWindow`](../FrmMain.cs));
  - переключение языка ([`FrmMain.buttonChangeLanguage_Click`](../FrmMain.cs));
  - управление сетевым контуром (инициализация [`ProductLine`](../ProductLine.cs), [`MyTimer`](../MyTimer.cs)).

## 2. Карта форм и навигация

### 2.1 Основной маршрут

1. Логин: [`FrmLogIn.btnLogin_Click`](../FrmLogIn.cs) -> [`DBHelper.UserlogIn`](../DBHelper.cs).  
2. Главная форма: [`FrmMain`](../FrmMain.cs), меню и управляющие кнопки.

### 2.2 Навигация из главной формы

| Точка входа в UI | Обработчик | Что открывается | Режим | Ограничения |
|---|---|---|---|---|
| Меню `Система -> Минимизировать` | [`MenuItemMin_Click`](../FrmMain.cs) | Сворачивание главного окна | - | Нет |
| Меню `Система -> Переключить язык` | [`buttonChangeLanguage_Click`](../FrmMain.cs) | Переключение `ru-RU <-> zh-CN` | - | Нет |
| Меню `Система -> Производственная доска` | [`生产看板ToolStripMenuItem_Click`](../FrmMain.cs) | [`FrmDisplay`](../FrmDisplay.cs) | `Show` | Открывается только если заполнен `plan` |
| Меню `Упр. линией -> Настр. персонала` | [`MenuItemPerson_Click`](../FrmMain.cs) | [`FrmUser`](../FrmUser.cs) | `Show` | Проверка прав через [`ShowWindow`](../FrmMain.cs) |
| Меню `Упр. линией -> Настр. продукта` | [`MenuItemProduct_Click`](../FrmMain.cs) | [`FrmProduct`](../FrmProduct.cs) | `Show` | Проверка прав через [`ShowWindow`](../FrmMain.cs) |
| Меню `Просм. данных -> Отсл. данных` | [`MenuItemReview_Click`](../FrmMain.cs) | [`FrmReview`](../FrmReview.cs) | `Show` | Проверка прав через [`ShowWindow`](../FrmMain.cs) |
| Меню `Просм. данных -> Сетевые данные` | [`MenuItemNet_Click`](../FrmMain.cs) | [`FrmNet`](../FrmNet.cs) | `Show` | Только после старта производства (`btnStart.Enabled == false`) |
| Меню `Упр. данными -> Настр. штрих-кода` | [`MenuItemBarcode_Click`](../FrmMain.cs) | [`FrmStandCode`](../UI/FrmStandCode.cs) | `ShowDialog` | Нет |
| Меню `Упр. данными -> Настр. отслеж.` | [`MenuItemReviewData_Click`](../FrmMain.cs) | [`FrmReviewData`](../FrmReviewData.cs) | `Show` | Проверка прав через [`ShowWindow`](../FrmMain.cs) |
| Меню `Упр. данными -> 档次号设置` | [`toolStripMenuItem1_Click`](../FrmMain.cs) | [`FormGradeNumber`](../UI/FormGradeNumber.cs) | `ShowDialog` | Нет |
| Кнопка `Настройки печати` | [`button2_Click`](../FrmMain.cs) | [`FrmSetPrintCode`](../FrmSetPrintCode.cs) | `ShowDialog` | Нет |
| Кнопка `Скан. № партии` | [`button3_Click`](../FrmMain.cs) | [`FrmVerifySCode`](../UI/FrmVerifySCode.cs) | `ShowDialog` | Требует выбранные `comProduct` и `cbgrade` |

### 2.3 Формы, присутствующие в коде, но не подключенные к текущему меню

- [`FrmStation`](../FrmStation.cs), [`FrmRS`](../FrmRS.cs), [`FrmGroup`](../FrmGroup.cs) имеют ветки в [`FrmMain.ShowWindow`](../FrmMain.cs), но не имеют активных пунктов меню в [`FrmMain.Designer`](../FrmMain.Designer.cs).

## 3. Роли и права доступа

### 3.1 Источник прав

- Проверка логина: [`DBHelper.UserlogIn`](../DBHelper.cs) (чтение `userLevel` из `userTable`).
- Первичный уровень пользователя передается в [`FrmMain`](../FrmMain.cs) из [`Program.Main`](../Program.cs).

### 3.2 Модель авторизации в UI

- Для защищенных форм используется [`FrmMain.ShowWindow`](../FrmMain.cs).
- Если `level < 1`, вызывается повторный логин-эскалация через [`FrmLog`](../FrmLog.cs), и доступ разрешается только при `frmLog.level > 1`.
- Если `level >= 1`, форма открывается без дополнительного запроса.

### 3.3 Практическое следствие

- В [`FrmUser`](../FrmUser.cs) уровни задаются как `SelectedIndex` (`0`/`1`).  
  При текущем условии `> 1` для эскалации это означает, что для operator-сценария нужен отдельный пользователь с уровнем выше `1` (или корректировка правила).

## 4. Зоны ответственности форм

- [`FrmMain`](../FrmMain.cs): оркестратор производственного цикла, языков, меню, открытия форм, подготовки параметров для сетевой обработки.
- [`FrmNet`](../FrmNet.cs): онлайн-мониторинг входящих/исходящих сигналов `StationData`/`MasterData`.
- [`FrmUser`](../FrmUser.cs): CRUD пользователей и уровней доступа.
- [`FrmProduct`](../FrmProduct.cs): настройка продукта и последовательности станций.
- [`FrmReview`](../FrmReview.cs): трассировка/поиск/выгрузка результатов.
- [`FrmReviewData`](../FrmReviewData.cs): настройка полей трассировки (`ReviewData`) по станциям.
- [`FrmStandCode`](../UI/FrmStandCode.cs): эталонные коды по деталям.
- [`FrmVerifySCode`](../UI/FrmVerifySCode.cs): сравнение скан-кодов с эталоном и сохранение `scancode`.
- [`FrmSetPrintCode`](../FrmSetPrintCode.cs): шаблон и генерация печатного кода.
- [`FrmDisplay`](../FrmDisplay.cs): производственный экран/дашборд.

## 5. Key handlers и ввод с клавиатуры

### 5.1 Глобальные/формовые

- [`FrmDisplay_KeyPress`](../FrmDisplay.cs): `Esc` закрывает дашборд.
- Включение перехвата клавиш в форме: [`FrmDisplay.Designer`](../FrmDisplay.Designer.cs) (`KeyPreview = true`).

### 5.2 Поля с валидацией ввода

- [`FrmProduct.txtID_KeyPress`](../FrmProduct.cs): только цифры и `.` через [`MyTools.CheckKey`](../MyTools.cs).
- [`FrmStation.txtIP_KeyPress`](../FrmStation.cs): только `0-9` и `.`.
- [`FrmStation.txtPort_KeyPress`](../FrmStation.cs): только `0-9`.
- [`FrmRS.txtDataBits_KeyPress`](../FrmRS.cs): только `0-9`.

### 5.3 Сканеры и фокус

- Для COM-сканеров данные приходят через `SerialPort.DataReceived`:
  - [`FrmBarcode.serialPort_DataReceived`](../FrmBarcode.cs),
  - [`FrmStandCode.SerialPort_DataReceived`](../UI/FrmStandCode.cs),
  - [`FrmVerifySCode.SerialPort_DataReceived`](../UI/FrmVerifySCode.cs).
- Запись идет в активный `TextBox` (зависимость от фокуса UI).

## 6. Интеграции UI с внешним контуром

| Интеграция | Где в UI/коде | Назначение |
|---|---|---|
| MySQL (ADO.NET) | [`DBHelper`](../DBHelper.cs), формы `Frm*` | CRUD/поиск/проверки, таблицы трассировки/конфигурации |
| SqlSugar ORM | [`SqlSugarHelper`](../Repository/SqlSugarHelper.cs), `FrmStandCode`, `FrmVerifySCode`, `FrmSetPrintCode`, `MyTimer` | типизированные операции с моделями |
| TCP Socket (станции) | [`ProductLine.startLisen`](../ProductLine.cs), [`AcceptSocketClient`](../ProductLine.cs), [`ReceiveData`](../ProductLine.cs), [`SendData`](../ProductLine.cs) | обмен с ПЛК/станциями |
| Таймер бизнес-цикла | [`MyTimer.Start`](../MyTimer.cs), [`DealNetUsedData`](../MyTimer.cs), [`DealNetDownLineUsedData`](../MyTimer.cs) | обработка статусов, запись результатов, обратная связь |
| Zebra печать по TCP | [`MyTimer.printCode`](../MyTimer.cs), дублирующий метод [`FrmMain.printCode`](../FrmMain.cs) | печать этикеток из `.prn` |
| SerialPort (сканеры) | [`FrmBarcode`](../FrmBarcode.cs), [`FrmStandCode`](../UI/FrmStandCode.cs), [`FrmVerifySCode`](../UI/FrmVerifySCode.cs) | ввод скан-кодов |
| Excel Export | [`FrmReview.btnToExcel_Click`](../FrmReview.cs), [`ExportDGVToExcel.ExportExcel`](../ExportDGVToExcel.cs) | выгрузка таблиц из DataGridView |
| События счетчиков | [`Utils/AppContext`](../Utils/AppContext.cs), [`FrmDisplay`](../FrmDisplay.cs) | обновление производственного табло |

## 7. Жизненный цикл окон и состояние

- Окна типа `FrmUser/FrmProduct/FrmReview/FrmNet/...` удерживаются как singleton-ссылки в [`FrmMain`](../FrmMain.cs) и сбрасываются в `null` при закрытии (например, [`FrmUser_FormClosed`](../FrmUser.cs), [`FrmReview_FormClosed`](../FrmReview.cs), [`FrmNet_FormClosed`](../FrmNet.cs)).
- Модальные формы (`ShowDialog`) открываются каждый раз заново и не кешируются.
- Состояние производства:
  - старт: [`btnStart_Click`](../FrmMain.cs) блокирует `groupBox`, запускает [`MyTimer`](../MyTimer.cs);
  - стоп: [`btnStop_Click`](../FrmMain.cs) в текущей реализации снимает блокировку только при `dealNetData == null`; при активном цикле показывает сообщение.

## 8. Риски и точки контроля для сопровождения UI

- Проверять согласованность матрицы прав (`level < 1` / `level > 1`) с реальными значениями `userLevel` в `userTable`.
- Поддерживать только подключенные маршруты меню; legacy-ветки (`Station/Group/RS`) либо вернуть в меню, либо удалить из маршрутизации.
- При изменениях сетевого цикла всегда верифицировать связку `FrmMain -> ProductLine -> MyTimer -> DBHelper`.


