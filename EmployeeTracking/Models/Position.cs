using EmployeeTracking.DTOs;

namespace EmployeeTracking.Models
{
    public class Position : IPosition
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;

        public static Position Create(PositionBindingModel model)
        {
            return new Position()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public void Update(PositionBindingModel model)
        {
            Name = model.Name;
        }

        public PositionViewModel GetViewModel => new()
        {
            Id = Id,
            Name = Name
        };
    }
}
