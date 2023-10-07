using EmployeeTracking.DTOs;
using EmployeeTracking.Models;

namespace EmployeeTracking.Storages
{
    public class EmployeeStorage : IEmployeeStorage
    {
        public EmployeeViewModel? Delete(EmployeeBindingModel model)
        {
            using var context = new EmployeeTrackingDatabase();
            var element = context.Employees.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Employees.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }

        public EmployeeViewModel? GetElement(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            using var context = new EmployeeTrackingDatabase();
            return context.Employees
                    .FirstOrDefault(x => id == x.Id)
                    ?.GetViewModel;
        }

        public List<EmployeeViewModel> GetFullList()
        {
            using var context = new EmployeeTrackingDatabase();
            return context.Employees
                    .OrderBy(x => x.Id)
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public EmployeeViewModel? Insert(EmployeeBindingModel model)
        {
            using var context = new EmployeeTrackingDatabase();
            model.Id = context.Employees.Count() > 0 ? context.Employees.Max(x => x.Id) + 1 : 1;
            var newEmployee = Employee.Create(model);
            if (newEmployee == null)
            {
                return null;
            }
            context.Employees.Add(newEmployee);
            context.SaveChanges();
            return newEmployee.GetViewModel;
        }

        public EmployeeViewModel? Update(EmployeeBindingModel model)
        {
            using var context = new EmployeeTrackingDatabase();
            var staff = context.Employees.FirstOrDefault(x => x.Id == model.Id);
            if (staff == null)
            {
                return null;
            }
            staff.Update(model);
            context.SaveChanges();
            return staff.GetViewModel;
        }
    }
}
