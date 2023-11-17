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

        public virtual DbSet<User> Users { get; set; } // Utilise le nom au pluriel pour la convention

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=probabilityx_db;User Id=root;Password=root;"); // Ajoute ta chaîne de connexion SQL Server ici
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SetUser();
        }
    }
}
