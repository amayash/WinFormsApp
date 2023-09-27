using DocumentFormat.OpenXml;
using System.ComponentModel;
using WinFormsLibrary.OfficePackage;
using WinFormsLibrary.OfficePackage.HelperEnums;
using WinFormsLibrary.OfficePackage.HelperModels;

namespace WinFormsLibrary
{
    public partial class ComponentHardTable : Component
    {
        private OpenXMLSaveToExcel saveToExcel;
        public ComponentHardTable()
        {
            InitializeComponent();
            saveToExcel = new OpenXMLSaveToExcel();
        }

        public ComponentHardTable(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            saveToExcel = new OpenXMLSaveToExcel();
        }
        /// <summary>
        /// Создает отчет с жестко настроенной таблицей в документе Excel на основе предоставленной информации.
        /// </summary>
        /// <typeparam name="T">Тип данных для таблицы.</typeparam>
        /// <param name="info">Информация о создании жестко настроенной таблицы в документе Excel.</param>
        public void CreateHardTableReport<T>(ExcelInfoTable<T> info)
        {
            if (string.IsNullOrEmpty(info.FileName) || string.IsNullOrEmpty(info.Title)
                || info.Titles.Length == 0 || info.Data.Length == 0 || info.ColumnWidth.Length == 0
                || info.Props.Length == 0 || info.RowHeight.Length == 0)
            {
                throw new ArgumentException("Неполные входные данные.");
            }

            if (info.Titles.Any(string.IsNullOrEmpty) || info.Props.Any(string.IsNullOrEmpty)
                || info.ColumnWidth.Any(x => x == 0) || info.RowHeight.Any(x => x == 0))
            {
                throw new ArgumentException("Неверные входные данные.");
            }

            if (info.Titles.Length != info.Props.Length)
            {
                throw new ArgumentException("Не для каждого столбца известно свойство/поле класса из которого для него следует брать значение.");
            }

            saveToExcel.CreateExcel(new() { FileName = info.FileName, Title = info.Title });

            // Устанавливаем ширину столбцов
            for (int i = 0; i < info.ColumnWidth.Length; i++)
            {
                saveToExcel.SetColumnWidth((uint)i + 1, info.ColumnWidth[i]);
            }

            saveToExcel.InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            uint rowIndex = 2;

            // Устанавливаем заголовки
            int columnIndex = 1;
            foreach (var title in info.Titles)
            {
                string columnLabel = GetColumnName(columnIndex);
                saveToExcel.InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = columnLabel,
                    RowIndex = rowIndex,
                    Text = title,
                    StyleInfo = ExcelStyleInfoType.TitleWithBorder
                });
                columnIndex++;
            }

            rowIndex++;

            foreach (var data in info.Data)
            {
                columnIndex = 1;

                foreach (string prop in info.Props)
                {
                    string propertyValue = GetPropertyValue(prop, data);
                    string columnLabel = GetColumnName(columnIndex);
                    saveToExcel.InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = columnLabel,
                        RowIndex = rowIndex,
                        Text = propertyValue,
                        StyleInfo = columnIndex == 1 ? ExcelStyleInfoType.TitleWithBorder : ExcelStyleInfoType.TextWithBorder
                    });
                    columnIndex++;
                }
                rowIndex++;
            }

            // Устанавливаем высоту строк
            for (int i = 0; i < info.RowHeight.Length; i++)
            {
                saveToExcel.SetRowHeight((uint)i + 2, info.RowHeight[i]);
            }

            saveToExcel.SaveExcel(new() { FileName = info.FileName, Title = info.Title });
        }

        /// <summary>
        /// Получает значение свойства объекта <typeparamref name="T"/> по его имени.
        /// </summary>
        /// <typeparam name="T">Тип объекта, из которого нужно получить значение свойства.</typeparam>
        /// <param name="propertyName">Имя свойства, значение которого нужно получить.</param>
        /// <param name="data">Объект, из которого будет получено значение свойства.</param>
        /// <returns>Значение свойства в виде строки или пустая строка, если свойство не найдено или его значение равно null.</returns>
        private string GetPropertyValue<T>(string propertyName, T data)
        {
            var propertyInfo = data.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                var value = propertyInfo.GetValue(data);
                return value?.ToString() ?? string.Empty;
            }

            return string.Empty;
        }
        /// <summary>
        /// Вспомогательная функция для преобразования числового индекса столбца в буквенное обозначение.
        /// </summary>
        /// <param name="columnIndex">Числовой индекс столбца.</param>
        /// <returns>Буквенное обозначение столбца (например, "A", "B", "AA", "AB" и так далее).</returns>
        private string GetColumnName(int columnIndex)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string columnName = "";

            while (columnIndex > 0)
            {
                int remainder = (columnIndex - 1) % 26;
                columnName = letters[remainder] + columnName;
                columnIndex = (columnIndex - 1) / 26;
            }

            return columnName;
        }
    }
}
