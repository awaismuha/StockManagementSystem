using System.ComponentModel.DataAnnotations;

namespace StockManagementSystem.Api.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
} 