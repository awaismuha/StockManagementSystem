using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManagementSystem.Api.Models
{
    public class GRN
    {
        [Key]
        public int GRNId { get; set; }
        [Required]
        public string Supplier { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = "Pending";
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public ICollection<GRNItem>? GRNItems { get; set; }
    }
} 