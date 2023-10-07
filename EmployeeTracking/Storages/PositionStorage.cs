using EmployeeTracking.DTOs;
using EmployeeTracking.Models;

namespace EmployeeTracking.Storages
{
    public class PositionStorage : IPositionStorage
    {
        public PositionViewModel? Delete(PositionBindingModel model)
        {
            using var context = new EmployeeTrackingDatabase();
            var element = context.Positions.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Positions.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }

        public void DeleteList(List<PositionBindingModel> model)
        {
            foreach (var elem in model)
                Delete(elem);
        }

        public PositionViewModel? GetElement(PositionBindingModel model)
        {
            if (model == null)
                return null;
            using var context = new EmployeeTrackingDatabase();
            if (model.Id != 0)
                return context.Positions
                    .FirstOrDefault(x => x.Id == model.Id)?
                    .GetViewModel;
            if (!string.IsNullOrEmpty(model.Name))
                return context.Positions
                    .FirstOrDefault(x => x.Name.Equals(model.Name))?
                    .GetViewModel;
            return null;
        }

        public List<PositionViewModel> GetFullList()
        {
            using var context = new EmployeeTrackingDatabase();
            return context.Positions
                    .OrderBy(x => x.Id)
                    .Select(x => x.GetViewModel)
                    .ToList();
        }

        public PositionViewModel? Insert(PositionBindingModel model)
        {
            using var context = new EmployeeTrackingDatabase();
            model.Id = context.Positions.Count() > 0 ? context.Positions.Max(x => x.Id) + 1 : 1;
            var newPosition = Position.Create(model);
            if (newPosition == null)
            {
                return null;
            }
            context.Positions.Add(newPosition);
            context.SaveChanges();
            return newPosition.GetViewModel;
        }

        public PositionViewModel? Update(PositionBindingModel model)
        {
            using var context = new EmployeeTrackingDatabase();
            var skill = context.Positions.FirstOrDefault(x => x.Id == model.Id);
            if (skill == null)
            {
                return null;
            }
            skill.Update(model);
            context.SaveChanges();
            return skill.GetViewModel;
        }
    }
}
