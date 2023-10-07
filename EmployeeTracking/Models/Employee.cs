using EmployeeTracking.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTracking.Models
{
    public class Employee : IEmployee
    {
        public int Id { get; private set; }
        public string FullName { get; set; } = string.Empty;
        // Последние 5 должностей. Должность хранится в БД, с сущностью не связана.
        public List<string> Positions { get; set; } = new();
        [NotMapped]
        public string PositionsStr { get; set; } = string.Empty;
        // Подразделение.
        public string Department { get; set; } = string.Empty;
        public int YearsOfWork { get; set; }

        public static Employee Create(EmployeeBindingModel model)
        {
            return new Employee()
            {
                Id = model.Id,
                FullName = model.FullName,
                Positions = model.Positions,
                PositionsStr = string.Join(", ", model.Positions),
                Department = model.Department,
                YearsOfWork = model.YearsOfWork,
            };
        }

        public void Update(EmployeeBindingModel model)
        {
            FullName = model.FullName;
            YearsOfWork = model.YearsOfWork;
            Positions = model.Positions;
            Department = model.Department;
            PositionsStr = string.Join(", ", model.Positions);
        }

        public EmployeeViewModel GetViewModel => new()
        {
            Id = Id,
            FullName = FullName,
            YearsOfWork = YearsOfWork,
            Positions = Positions,
            PositionsStr = string.Join(", ", Positions),
            Department = Department
        };
    }
}
