using System;
using System.Collections.Generic;

namespace StockManagementSystem.Web.Models
{
    public class GRNViewModel
    {
        public int GRNId { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public List<GRNItemViewModel> GRNItems { get; set; } = new();
    }
    public class GRNItemViewModel
    {
        public int GRNItemId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
    }
} 