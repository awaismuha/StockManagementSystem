using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagementSystem.Api.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string SKU { get; set; } = string.Empty;
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [Required]
        public string UOM { get; set; } = string.Empty;
        public int StockQty { get; set; }
        public int ReorderLevel { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
    }
} 