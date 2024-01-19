using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProbabilityX_API.Models
{
    public class FavoriteCompanyModel
    {
        [Key]
        [Column(Order = 1)]
        public int Id_Company { get; set; }

        [Key]
        [Column(Order = 2)]
        public int Id_User { get; set; }

        // Ajoutez d'autres propriétés si nécessaire
    }
}
