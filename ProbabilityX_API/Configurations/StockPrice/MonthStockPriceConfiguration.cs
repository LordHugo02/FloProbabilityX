using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Configurations.StockPrice
{
    public static class MonthStockPriceConfiguration
    {
        public static void SetMonthStockPrice(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockPriceModel>(entity =>
            {
                entity.ConfigureStockPrice();
                entity.ToTable("month_stock_price");
            });
        }
    }
}
