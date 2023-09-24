namespace WinFormsLibrary.OfficePackage.HelperModels
{
    public class ExcelInfoTable<T>
    {
        public string FileName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string[] Titles { get; set; } = Array.Empty<string>();

        public T[] Data { get; set; } = Array.Empty<T>();

        public string[] Props { get; set; } = Array.Empty<string>();

        public double[] RowHeight { get; set; } = Array.Empty<double>();

        public double[] ColumnWidth { get; set; } = Array.Empty<double>();
    }
}
