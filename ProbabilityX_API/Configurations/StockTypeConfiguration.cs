using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Configurations
{
    public static class StockTypeConfiguration
    {
        public static void SetStockType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockTypeModel>(entity =>
            {
                entity.ToTable("stock_type");

                entity.Property(x => x.Id_StockType)
                    .HasColumnName("id_stock_type")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INT UNSIGNED")
                    .IsRequired();

                entity.Property(x => x.Name_StockType)
                    .HasColumnName("name_stock_type")
                    .HasMaxLength(255)
                    .IsRequired();


                entity.HasKey(x => x.Id_StockType);

                // Ajoutez d'autres configurations si nécessaire
            });
        }
    }
}
