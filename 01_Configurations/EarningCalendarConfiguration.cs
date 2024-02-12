using Microsoft.EntityFrameworkCore;
using ProbabilityX_API.Models;

namespace ProbabilityX_API.Configurations
{
	public static class EarningCalendarConfiguration
	{
		public static void SetEarningCalendar(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EarningCalendarModel>(entity =>
			{
				entity.ToTable("earnings_calendar");

				entity.Property(x => x.Id_Earning)
					.HasColumnName("id_earning")
					.ValueGeneratedOnAdd()
					.HasColumnType("INT UNSIGNED")
					.IsRequired();

				entity.Property(x => x.Id_Company)
					.HasColumnName("id_company")
					.HasColumnType("INT UNSIGNED")
					.IsRequired();

				entity.Property(x => x.BeneficePerAction)
					.HasColumnName("benefice_per_action")
                    .HasMaxLength(15);

                entity.Property(x => x.ForecastBeneficePerAction)
					.HasColumnName("forecast_benefice_per_action")
                    .HasMaxLength(15);

                entity.Property(x => x.Revenue)
					.HasColumnName("revenue")
					.HasMaxLength(15); // Ajustez la taille selon vos besoins

				entity.Property(x => x.ForecastRevenue)
					.HasColumnName("forecast_revenue")
					.HasMaxLength(15); // Ajustez la taille selon vos besoins

				entity.Property(x => x.ResultDate)
					.HasColumnName("result_date")
					.HasColumnType("DATE")
					.IsRequired();
				
				entity.Property(x => x.PeriodDate)
					.HasColumnName("period_date")
					.HasColumnType("DATE")
					.IsRequired();

				entity.Property(x => x.ResultTime)
					.HasColumnName("result_time")
					.HasColumnType("TIME");

				// Définition de la clé primaire
				entity.HasKey(x => x.Id_Earning);

				// Clé étrangère vers la table company
				entity.HasOne(x => x.Company)
					.WithMany()
					.HasForeignKey(x => x.Id_Company)
					.HasConstraintName("FK_earnings_calendar_company")
					.OnDelete(DeleteBehavior.NoAction);
			});
		}
	}
}
