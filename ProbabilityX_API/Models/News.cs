using System;
using System.ComponentModel.DataAnnotations;

namespace ProbabilityX_API.Models
{
    public class News
    {
        [Key]
        public int Id_News { get; set; }

        public int Id_Company { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime NewsDate { get; set; }
    }
}
