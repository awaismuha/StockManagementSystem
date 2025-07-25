using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagementSystem.Api.Models
{
    public class GINItem
    {
        [Key]
        public int GINItemId { get; set; }
        [ForeignKey("GIN")]
        public int GINId { get; set; }
        public GIN? GIN { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Qty { get; set; }
    }
} 