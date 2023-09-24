using System.ComponentModel;
using WinFormsLibrary.OfficePackage;
using WinFormsLibrary.OfficePackage.HelperEnums;
using WinFormsLibrary.OfficePackage.HelperModels;

namespace WinFormsLibrary
{
    public partial class ComponentTable : Component
    {
        private OpenXMLSaveToExcel saveToExcel;
        public ComponentTable()
        {
            InitializeComponent();
            saveToExcel = new OpenXMLSaveToExcel();
        }

        public ComponentTable(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            saveToExcel = new OpenXMLSaveToExcel();
        }
        /// <summary>
        /// Создает отчет с таблицей в документе Excel на основе предоставленной информации.
        /// </summary>
        /// <param name="info">Информация о создании таблицы в документе Excel.</param>
        public void CreateTableReport(ExcelInfo info)
        {
            if (string.IsNullOrEmpty(info.FileName) || string.IsNullOrEmpty(info.Title) || info.Tables.Count == 0)
            {
                throw new ArgumentException("Неполные входные данные.");
            }

            if (info.Tables.Any(x => x.Length == 0) || info.Tables.Any(x =>
            {
                int rowCount = x.GetLength(0);
                int colCount = x.GetLength(1);
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if (string.IsNullOrEmpty(x[i, j]))
                        {
                            return true; // Найдена пустая строка в массиве
                        }
                    }
                }
                return false; // Пустых строк в массиве не найдено
            }))
            {
                throw new ArgumentException("Неверные входные данные.");
            }

            saveToExcel.CreateExcel(info);

            saveToExcel.InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });

            uint rowIndex = 2;
            foreach (var table in info.Tables)
            {
                int rows = table.GetLength(0);    // Количество строк
                int columns = table.GetLength(1); // Количество столбцов
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        char columnLabel = (char)('A' + j);
                        saveToExcel.InsertCellInWorksheet(new ExcelCellParameters
                        {
                            ColumnName = columnLabel.ToString(),
                            RowIndex = rowIndex,
                            Text = table[i, j],
                            StyleInfo = ExcelStyleInfoType.TextWithBorder
                        });
                    }
                    rowIndex++;
                }
            }
            saveToExcel.SaveExcel(info);

        }
    }
}