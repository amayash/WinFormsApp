using EmployeeTracking.Models;

namespace EmployeeTracking.DTOs
{
    public class PositionViewModel : IPosition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
