using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Configurations.StockPrice
{
    public static class HourStockPriceConfiguration
    {
        public static void SetHourStockPrice(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockPriceModel>(entity =>
            {
                entity.ConfigureStockPrice();
                entity.ToTable("hour_stock_price");
                // Autres configurations spécifiques à HourStockPrice
            });
        }
    }
}
