using WinFormsLibrary.OfficePackage.HelperEnums;

namespace WinFormsLibrary.OfficePackage.HelperModels
{
    public class ExcelInfoChart
    {
        public string FileName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string TitleChart { get; set; } = string.Empty;

        public LegendPosition LegendPosition { get; set; } = LegendPosition.Bottom;

        public string[] SeriesNames { get; set; } = Array.Empty<string>();

        public double[] Data { get; set; } = Array.Empty<double>();
    }
}
