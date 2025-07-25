using System;

namespace StockManagementSystem.Web.Models
{
    public class AuditLogViewModel
    {
        public int AuditLogId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Entity { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public string? UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Details { get; set; }
    }
} 