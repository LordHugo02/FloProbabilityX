using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Configurations
{
    public static class NewsConfiguration
    {
        public static void SetNews(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("news");

                entity.Property(x => x.Id_News)
                    .HasColumnName("id_news")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INT UNSIGNED")
                    .IsRequired();

                entity.Property(x => x.Id_Company)
                    .HasColumnName("id_company")
                    .HasColumnType("INT UNSIGNED")
                    .IsRequired();

                entity.Property(x => x.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(x => x.Content)
                    .HasColumnName("content")
                    .HasColumnType("TEXT")
                    .IsRequired();

                entity.Property(x => x.NewsDate)
                    .HasColumnName("news_date")
                    .HasColumnType("DATETIME")
                    .IsRequired();

                // Définition de la clé primaire
                entity.HasKey(x => x.Id_News);
            });
        }
    }
}
