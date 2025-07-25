using System.ComponentModel.DataAnnotations;

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
        public decimal TotalAmount { get; set; }
        public ICollection<GRNItem>? GRNItems { get; set; }
    }
} 