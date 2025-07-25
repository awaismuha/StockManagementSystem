using System;
using System.Collections.Generic;

namespace StockManagementSystem.Web.Models
{
    public class StockItemViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string? Category { get; set; }
        public string UOM { get; set; } = string.Empty;
        public int StockQty { get; set; }
        public int ReorderLevel { get; set; }
    }
    public class StockMovementViewModel
    {
        public List<GRNViewModel> GRNs { get; set; } = new();
        public List<GINViewModel> GINs { get; set; } = new();
    }
} 