using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WarehouseTraineeTask.Domain.Entity;

namespace WarehouseTraineeTask.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(255);

            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.AuthProvider)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(u => u.ProviderUserId)
                .HasMaxLength(255)
                .IsRequired(false);
        }
    }
}