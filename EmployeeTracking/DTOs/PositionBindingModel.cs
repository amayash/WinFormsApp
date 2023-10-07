using EmployeeTracking.Models;

namespace EmployeeTracking.DTOs
{
    public class PositionBindingModel : IPosition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
