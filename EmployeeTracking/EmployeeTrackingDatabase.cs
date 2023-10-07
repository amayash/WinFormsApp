using EmployeeTracking.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTracking
{
    public class EmployeeTrackingDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=EmployeeTrackingDatabase;Username=user;Password=12345");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Employee> Employees { set; get; }
        public virtual DbSet<Position> Positions { set; get; }
    }
}