using System;
using System.Collections.Generic;

namespace StockManagementSystem.Web.Models
{
    public class GINViewModel
    {
        public int GINId { get; set; }
        public string Recipient { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? Reason { get; set; }
        public List<GINItemViewModel> GINItems { get; set; } = new();
    }
    public class GINItemViewModel
    {
        public int GINItemId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Qty { get; set; }
    }
} 