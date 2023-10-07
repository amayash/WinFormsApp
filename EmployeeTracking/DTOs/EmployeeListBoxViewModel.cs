namespace EmployeeTracking.DTOs
{
    public class EmployeeListBoxViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PositionsStr { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public int YearsOfWork { get; set; }
    }
}
