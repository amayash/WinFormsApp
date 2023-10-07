using EmployeeTracking.Models;

namespace EmployeeTracking.DTOs
{
    public class EmployeeBindingModel : IEmployee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public List<string> Positions { get; set; } = new();
        public int YearsOfWork { get; set; }
        public string Department { get; set; } = string.Empty;
    }
}
