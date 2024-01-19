using System.ComponentModel.DataAnnotations;

namespace ProbabilityX_API.Models
{
    public class CompanyModel
    {
        [Key]
        public int Id_Company { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string StockSymbol { get; set; } = string.Empty;
        public int Id_StockType { get; set; }
    }
}
