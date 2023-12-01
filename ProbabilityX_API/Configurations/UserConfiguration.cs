using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Configurations
{
    public static class UserConfiguration
    {
        public static void SetUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(x => x.Id_User)
                    .HasColumnName("id_user")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INT UNSIGNED")
                    .IsRequired();

                entity.Property(x => x.FirstName)
                    .HasColumnName("firstname")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(x => x.LastName)
                    .HasColumnName("lastname")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(x => x.Username)
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(x => x.Password)
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(x => x.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(x => x.Role)
                    .HasColumnName("role")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(x => x.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(255)
                    .IsRequired(false);

                entity.Property(x => x.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("DATE")
                    .IsRequired();

                entity.Property(x => x.Country)
                    .HasColumnName("country")
                    .HasColumnType("INT")
                    .IsRequired();

                entity.Property(x => x.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("DATE")
                    .IsRequired();

                entity.Property(x => x.ModifiedAt)
                    .HasColumnName("modified_at")
                    .HasColumnType("DATE")
                    .IsRequired();

                entity.Property(x => x.IsRgpdAccepted)
                    .HasColumnName("is_rgpd_accepted")
                    .HasColumnType("TINYINT")
                    .IsRequired();

                entity.Property(x => x.Language)
                    .HasColumnName("language")
                    .HasColumnType("INT")
                    .IsRequired();

            });
        }
    }
}

