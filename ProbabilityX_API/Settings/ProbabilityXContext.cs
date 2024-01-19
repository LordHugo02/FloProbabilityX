using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Configurations;
using ProbabilityX_API.Configurations.StockPrice;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Settings
{
    public partial class ProbabilityXContext : DbContext
    {
        public ProbabilityXContext()
        {

        }

        public ProbabilityXContext(DbContextOptions<ProbabilityXContext> options) : base(options) { }

        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<CompanyModel> Companies { get; set; }
        public virtual DbSet<FavoriteCompanyModel> FavoriteCompanies { get; set; }
        public virtual DbSet<StockPriceModel> HourStockPrices { get; set; }
        public virtual DbSet<StockPriceModel> MinuteStockPrices { get; set; }
        public virtual DbSet<StockPriceModel> WeekStockPrices { get; set; }
        public virtual DbSet<StockPriceModel> MonthStockPrices { get; set; }
        public virtual DbSet<NewsModel> News { get; set; }
        public virtual DbSet<StockTypeModel> StockTypes { get; set; }

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
