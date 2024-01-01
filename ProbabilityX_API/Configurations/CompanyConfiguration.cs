using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProbabilityX_API.Configurations
{
    public static class CompanyConfiguration
    {
        public static void SetCompany(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(x => x.Id_Company)
                    .HasColumnName("id_company")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INT UNSIGNED")
                    .IsRequired();

                entity.Property(x => x.CompanyName)
                    .HasColumnName("company_name")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(x => x.StockSymbol)
                    .HasColumnName("stock_symbol")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(x => x.Id_StockType)
                    .HasColumnName("id_stock_type")
                    .HasColumnType("INT UNSIGNED")
                    .IsRequired();

                // Définition de la clé primaire
                entity.HasKey(x => x.Id_Company);

            });
        }
    }
}
