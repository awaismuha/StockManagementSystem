using System.ComponentModel.DataAnnotations;

namespace StockManagementSystem.Api.Models
{
    public class AuditLog
    {
        [Key]
        public int AuditLogId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Entity { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public string? UserId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? Details { get; set; }
    }
} 