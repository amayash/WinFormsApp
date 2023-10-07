namespace EmployeeTracking.Models
{
    public class Department : IDepartment
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
    }
}
