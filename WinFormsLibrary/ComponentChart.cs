using System.ComponentModel;
using WinFormsLibrary.OfficePackage;
using WinFormsLibrary.OfficePackage.HelperModels;

namespace WinFormsLibrary
{
    public partial class ComponentChart : Component
    {
        private SpireSaveToExcel saveToExcel;
        public ComponentChart()
        {
            InitializeComponent();
            saveToExcel = new();
        }

        public ComponentChart(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            saveToExcel = new();
        }
        /// <summary>
        /// Создает отчет с графиком в документе Excel на основе предоставленной информации.
        /// </summary>
        /// <param name="info">Информация о создании графика в документе Excel.</param>
        public void CreateChartReport(ExcelInfoChart info)
        {
            saveToExcel.SaveBarChartToExcel(info);
        }
    }
}
