using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Configurations
{
    public static class FavoriteCompanyConfiguration
    {
        public static void SetFavoriteCompany(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteCompany>(entity =>
            {
                entity.ToTable("favorites_company");

                entity.Property(x => x.Id_Company)
                    .HasColumnName("id_company")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INT UNSIGNED")
                    .IsRequired();

                entity.Property(x => x.Id_User)
                    .HasColumnName("id_user")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INT UNSIGNED")
                    .IsRequired();

               entity.HasKey(x => new { x.Id_Company, x.Id_User });
            });
        }
    }
}
