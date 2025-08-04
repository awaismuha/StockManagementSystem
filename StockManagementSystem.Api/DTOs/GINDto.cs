namespace StockManagementSystem.Api.DTOs
{
    public class GINDto
    {
        public int GINId { get; set; }
        public string Recipient { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? Reason { get; set; }
        public List<GINItemDto>? GINItems { get; set; }
    }

    public class GINItemDto
    {
        public int GINItemId { get; set; }
        public int GINId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductSKU { get; set; } = string.Empty;
        public int Qty { get; set; }
    }

    public class CreateGINDto
    {
        public string Recipient { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? Reason { get; set; }
        public List<CreateGINItemDto>? GINItems { get; set; }
    }

    public class CreateGINItemDto
    {
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }
} 