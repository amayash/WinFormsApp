using EmployeeTracking.DTOs;

namespace EmployeeTracking.Storages
{
    public interface IPositionStorage
    {
        PositionViewModel? Delete(PositionBindingModel model);

        void DeleteList(List<PositionBindingModel> model);

        List<PositionViewModel> GetFullList();

        PositionViewModel? GetElement(PositionBindingModel model);

        PositionViewModel? Insert(PositionBindingModel model);

        PositionViewModel? Update(PositionBindingModel model);
    }
}
