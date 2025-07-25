using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagementSystem.Api.Models
{
    public class GRNItem
    {
        [Key]
        public int GRNItemId { get; set; }
        [ForeignKey("GRN")]
        public int GRNId { get; set; }
        public GRN? GRN { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
    }
} 