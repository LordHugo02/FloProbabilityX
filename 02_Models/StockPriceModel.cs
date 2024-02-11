using System;
using System.ComponentModel.DataAnnotations;

namespace ProbabilityX_API.Models
{
    public class StockPriceModel
    {
        [Key]
        public int Id_Stock_Price { get; set; }

        public int Id_Company { get; set; }

        public DateTime Date_Price { get; set; }

        public decimal OpenPrice { get; set; }

        public decimal ClosePrice { get; set; }

        public decimal HighPrice { get; set; }

        public decimal LowPrice { get; set; }
    }
}
