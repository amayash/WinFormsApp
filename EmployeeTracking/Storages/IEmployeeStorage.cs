using EmployeeTracking.DTOs;

namespace EmployeeTracking.Storages
{
    public interface IEmployeeStorage
    {
        EmployeeViewModel? Delete(EmployeeBindingModel model);

        EmployeeViewModel? GetElement(int id);

        List<EmployeeViewModel> GetFullList();

        EmployeeViewModel? Insert(EmployeeBindingModel model);

        EmployeeViewModel? Update(EmployeeBindingModel model);
    }
}
