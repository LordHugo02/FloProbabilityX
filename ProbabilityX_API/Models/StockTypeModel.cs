using System.ComponentModel.DataAnnotations;

namespace ProbabilityX_API.Models
{
    public class StockTypeModel
    {
        [Key]
        public int Id_StockType { get; set; }

        public string Name_StockType { get; set; } = string.Empty;

        // Ajoutez d'autres propriétés si nécessaire
    }
}
