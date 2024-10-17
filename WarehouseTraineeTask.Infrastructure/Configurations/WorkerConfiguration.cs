using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarehouseTraineeTask.Domain.Entity;

namespace WarehouseTraineeTask.Infrastructure.Configurations
{
    public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(w => w.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(w => w.Email)
                .HasMaxLength(100);

            builder.Property(w => w.Phone)
                .HasMaxLength(20);

            builder.Property(w => w.Position)
                .HasMaxLength(50);

            builder.Property(w => w.HireDate)
                .IsRequired();

            builder.HasMany(w => w.WorkerDepartments)
                .WithOne(wd => wd.Worker)
                .HasForeignKey(wd => wd.WorkerId);
        }
    }
}