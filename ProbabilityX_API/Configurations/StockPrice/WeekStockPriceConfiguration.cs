using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Configurations.StockPrice
{
    public static class WeekStockPriceConfiguration
    {
        public static void SetWeekStockPrice(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockPriceModel>(entity =>
            {
                entity.ConfigureStockPrice();
                entity.ToTable("week_stock_price");

            });
        }
    }
}

