using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Configurations;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Settings
{
    public partial class ProbabilityXContext : DbContext
    {
        public ProbabilityXContext()
        {

        }

        public ProbabilityXContext(DbContextOptions<ProbabilityXContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<FavoriteCompany> FavoriteCompanies { get; set; }
        public virtual DbSet<StockPrice> HourStockPrices { get; set; }
        public virtual DbSet<StockPrice> MinuteStockPrices { get; set; }
        public virtual DbSet<StockPrice> WeekStockPrices { get; set; }
        public virtual DbSet<StockPrice> MonthStockPrices { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<StockType> StockTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SetUser();
            modelBuilder.SetCompany();
            modelBuilder.SetFavoriteCompany();
            modelBuilder.SetHourStockPrice();
            modelBuilder.SetMinuteStockPrice();
            modelBuilder.SetWeekStockPrice();
            modelBuilder.SetMonthStockPrice();
            modelBuilder.SetNews();
            modelBuilder.SetStockType();
        }
    }
}
