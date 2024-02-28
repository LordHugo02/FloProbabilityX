using System.ComponentModel.DataAnnotations;

namespace ProbabilityX_API.Models
{
    public class TopMovementOfTheDay
    {
        [Key]
        public int Id_TopMovementOfTheDay { get; set; }
        public int Id_Company { get; set; }
        public int Movement_Pourcentage_Start_Of_Day { get; set; }
        public int Movement_Pourcentage_End_Of_Day { get; set;}
        public int Movement_Pourcentage_Benefice_Possible { get; set; }
    }
}
