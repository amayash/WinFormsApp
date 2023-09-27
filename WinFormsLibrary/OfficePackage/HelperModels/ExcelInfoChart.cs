using WinFormsLibrary.OfficePackage.HelperEnums;

namespace WinFormsLibrary.OfficePackage.HelperModels
{
    public class ExcelInfoChart
    {
        public string FileName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string TitleChart { get; set; } = string.Empty;

        public LegendPosition LegendPosition { get; set; } = LegendPosition.Bottom;

        public List<(string Series, double Value)> SeriesAndData { get; set; } = new();
    }
}
