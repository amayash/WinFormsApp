using Spire.Xls;
using WinFormsLibrary.OfficePackage.HelperEnums;
using WinFormsLibrary.OfficePackage.HelperModels;

namespace WinFormsLibrary.OfficePackage
{
    public class SpireSaveToExcel
    {
        /// <summary>
        /// Сохраняет данные гистограммы в документ Excel.
        /// </summary>
        /// <param name="info">Информация о данных для гистограммы и параметрах сохранения.</param>
        public void SaveBarChartToExcel(ExcelInfoChart info)
        {
            // Проверка наличия данных
            if (string.IsNullOrEmpty(info.FileName) ||
                string.IsNullOrEmpty(info.Title) ||
                string.IsNullOrEmpty(info.TitleChart) ||
                info.SeriesNames.Length == 0 ||
                info.Data.Length == 0)
            {
                throw new ArgumentException("Неполные входные данные.");
            }
            if (info.SeriesNames.Any(string.IsNullOrEmpty))
            {
                throw new ArgumentException("Неверные входные данные.");
            }

            // Создание нового документа Excel
            Workbook workbook = new Workbook();
            Worksheet sheetChart = workbook.Worksheets[0];
            Worksheet sheetData = workbook.Worksheets[1];

            sheetChart.Range["A1"].Text = info.Title;
            sheetChart.Range["A1"].Style.Font.IsBold = true;

            // Вставка данных в таблицу
            sheetData.Range["A1"].Text = "Серия";
            sheetData.Range["B1"].Text = "Значение";

            for (int i = 0; i < info.SeriesNames.Length; i++)
            {
                sheetData.Range["A" + (i + 2)].Text = info.SeriesNames[i];
                sheetData.Range["B" + (i + 2)].NumberValue = info.Data[i];
            }

            // Добавление гистограммы
            Chart chart = sheetChart.Charts.Add(ExcelChartType.ColumnClustered);
            chart.DataRange = sheetData.Range["A1:B" + (info.SeriesNames.Length + 1)];
            chart.ChartTitle = info.TitleChart;
            chart.TopRow = 2;

            // Установка позиции легенды
            switch (info.LegendPosition)
            {
                case LegendPosition.Top:
                    chart.Legend.Position = LegendPositionType.Top;
                    break;
                case LegendPosition.Bottom:
                    chart.Legend.Position = LegendPositionType.Bottom;
                    break;
                case LegendPosition.Left:
                    chart.Legend.Position = LegendPositionType.Left;
                    break;
                case LegendPosition.Right:
                    chart.Legend.Position = LegendPositionType.Right;
                    break;
            }

            // Сохранение документа Excel
            workbook.SaveToFile(info.FileName, ExcelVersion.Version2013);
        }
    }
}
