using System.ComponentModel;
using WinFormsLibrary.OfficePackage;
using WinFormsLibrary.OfficePackage.HelperEnums;
using WinFormsLibrary.OfficePackage.HelperModels;

namespace WinFormsLibrary
{
    public partial class ComponentTable : Component
    {
        SaveToExcel saveToExcel;
        public ComponentTable()
        {
            InitializeComponent();
            saveToExcel = new SaveToExcel();
        }

        public ComponentTable(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            saveToExcel = new SaveToExcel();
        }

        public void CreateTableReport(List<ExcelInfo> infos)
        {
            foreach (var info in infos)
            {
                if (string.IsNullOrEmpty(info.FileName) || string.IsNullOrEmpty(info.Title) || info.Tables.Count == 0)
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
}