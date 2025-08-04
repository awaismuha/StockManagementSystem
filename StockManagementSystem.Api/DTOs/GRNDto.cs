namespace StockManagementSystem.Api.DTOs
{
    public class GRNDto
    {
        public int GRNId { get; set; }
        public string Supplier { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public List<GRNItemDto>? GRNItems { get; set; }
    }

    public class GRNItemDto
    {
        public int GRNItemId { get; set; }
        public int GRNId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductSKU { get; set; } = string.Empty;
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class CreateGRNDto
    {
        public string Supplier { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Status { get; set; } = "Pending";
        public List<CreateGRNItemDto>? GRNItems { get; set; }
    }

    public class CreateGRNItemDto
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
    }
} 