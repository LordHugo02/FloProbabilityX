using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Configurations.StockPrice
{
    public static class MinuteStockPriceConfiguration
    {
        public static void SetMinuteStockPrice(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockPriceModel>(entity =>
            {
                entity.ConfigureStockPrice();
                entity.ToTable("minute_stock_price");
                // Autres configurations spécifiques à MinuteStockPrice
            });
        }
    }
}
