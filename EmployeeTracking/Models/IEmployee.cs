namespace EmployeeTracking.Models
{
    public interface IEmployee
    {
        public int Id { get; }
        public string FullName { get; }
        // Последние 5 должностей. Должность хранится в БД, с сущностью не связана.
        public List<string> Positions { get; }
        // Подразделение.
        public string Department { get; }
        public int YearsOfWork { get; }
    }
}
