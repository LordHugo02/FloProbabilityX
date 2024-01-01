using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Configurations
{
    public static class MinuteStockPriceConfiguration
    {
        public static void SetMinuteStockPrice(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockPrice>(entity =>
            {
                entity.ToTable("minute_stock_price");


                entity.Property(x => x.Id_Stock_Price)
                    .HasColumnName("id_stock_price")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INT UNSIGNED")
                    .IsRequired();

                entity.Property(x => x.Id_Company)
                    .HasColumnName("id_company")
                    .HasColumnType("INT UNSIGNED")
                    .IsRequired();

                entity.Property(x => x.Date_Price)
                    .HasColumnName("date_price")
                    .HasColumnType("TIMESTAMP")
                    .IsRequired();

                entity.Property(x => x.OpenPrice)
                    .HasColumnName("open_price")
                    .HasColumnType("DECIMAL(10,2) UNSIGNED")
                    .IsRequired();

                entity.Property(x => x.ClosePrice)
                    .HasColumnName("close_price")
                    .HasColumnType("DECIMAL(10,2) UNSIGNED")
                    .IsRequired();

                entity.Property(x => x.HighPrice)
                    .HasColumnName("high_price")
                    .HasColumnType("DECIMAL(10,2) UNSIGNED")
                    .IsRequired();

                entity.Property(x => x.LowPrice)
                    .HasColumnName("low_price")
                    .HasColumnType("DECIMAL(10,2) UNSIGNED")
                    .IsRequired();

                // Définition de la clé primaire
                entity.HasKey(x => x.Id_Stock_Price);
            });
        }
    }
}

