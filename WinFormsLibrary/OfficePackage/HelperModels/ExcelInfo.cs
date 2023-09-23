namespace WinFormsLibrary.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public List<string[,]> Tables { get; set; } = new();
    }
}