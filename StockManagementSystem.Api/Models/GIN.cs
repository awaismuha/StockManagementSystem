using System.ComponentModel.DataAnnotations;

namespace StockManagementSystem.Api.Models
{
    public class GIN
    {
        [Key]
        public int GINId { get; set; }
        [Required]
        public string Recipient { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? Reason { get; set; }
        public ICollection<GINItem>? GINItems { get; set; }
    }
} 