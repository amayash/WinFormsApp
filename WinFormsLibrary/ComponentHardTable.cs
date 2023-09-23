using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel;
using WinFormsLibrary.OfficePackage;
using WinFormsLibrary.OfficePackage.HelperEnums;
using WinFormsLibrary.OfficePackage.HelperModels;

namespace WinFormsLibrary
{
    public partial class ComponentHardTable : Component
    {
        SaveToExcel saveToExcel;
        public ComponentHardTable()
        {
            InitializeComponent();
            saveToExcel = new SaveToExcel();
        }

        public ComponentHardTable(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            saveToExcel = new SaveToExcel();
        }

        public void CreateHardTableReport<T>(ExcelInfoTable<T> info)
        {
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
            char columnLabel = 'A';
            foreach (var title in info.Titles)
            {
                saveToExcel.InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = columnLabel.ToString(),
                    RowIndex = rowIndex,
                    Text = title,
                    StyleInfo = ExcelStyleInfoType.Title
                });
                columnLabel++;
            }

            rowIndex++;

            foreach (var data in info.Data)
            {
                columnLabel = 'A';

                foreach (string prop in info.Props)
                {
                    string propertyValue = GetPropertyValue(prop, data);
                    saveToExcel.InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = columnLabel.ToString(),
                        RowIndex = rowIndex,
                        Text = propertyValue,
                        StyleInfo = columnLabel == 'A' ? ExcelStyleInfoType.Title : ExcelStyleInfoType.TextWithBorder
                    });
                    columnLabel++;
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
    }
}
