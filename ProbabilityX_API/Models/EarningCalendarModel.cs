using System.ComponentModel.DataAnnotations;

namespace ProbabilityX_API.Models
{
    public class EarningCalendarModel
    {
        public int Id_Earning { get; set; }
        public int Id_Company { get; set; }
        public float? BeneficePerAction { get; set; }
        public float? ForecastBeneficePerAction { get; set; }
        public string Revenue { get; set; }
        public string ForecastRevenue { get; set; }
        public DateTime ResultDate { get; set; }
        public TimeSpan? ResultTime { get; set; }

        // Navigation property for the foreign key relationship
        public CompanyModel Company { get; set; }
    }
}
