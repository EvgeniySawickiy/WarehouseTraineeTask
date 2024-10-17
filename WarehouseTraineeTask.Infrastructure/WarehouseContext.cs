using Microsoft.EntityFrameworkCore;
using WarehouseTraineeTask.Domain.Entity;
using WarehouseTraineeTask.Infrastructure.Configurations;

namespace WarehouseTraineeTask.Infrastructure
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext(DbContextOptions<WarehouseContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WorkerDepartment> WorkerDepartments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new WorkerDepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}