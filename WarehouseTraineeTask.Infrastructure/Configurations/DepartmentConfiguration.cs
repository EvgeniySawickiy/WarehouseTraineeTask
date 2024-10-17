using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarehouseTraineeTask.Domain.Entity;

namespace WarehouseTraineeTask.Infrastructure.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Location)
                .HasMaxLength(200);

            builder.Property(d => d.Capacity)
                .IsRequired();

            builder.Property(d => d.CreatedDate);

            builder.HasMany(d => d.Products)
                .WithOne(p => p.Department)
                .HasForeignKey(p => p.DepartmentId);

            builder.HasMany(d => d.WorkerDepartments)
                .WithOne(wd => wd.Department)
                .HasForeignKey(wd => wd.DepartmentId);
        }
    }
}