using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarehouseTraineeTask.Domain.Entity;

namespace WarehouseTraineeTask.Infrastructure.Configurations
{
    public class WorkerDepartmentConfiguration : IEntityTypeConfiguration<WorkerDepartment>
    {
        public void Configure(EntityTypeBuilder<WorkerDepartment> builder)
        {
            builder.HasKey(wd => new { wd.WorkerId, wd.DepartmentId });

            builder.HasOne(wd => wd.Worker)
                .WithMany(w => w.WorkerDepartments)
                .HasForeignKey(wd => wd.WorkerId);

            builder.HasOne(wd => wd.Department)
                .WithMany(d => d.WorkerDepartments)
                .HasForeignKey(wd => wd.DepartmentId);
        }
    }
}