namespace StockManagementSystem.Api.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string UOM { get; set; } = string.Empty;
        public int StockQty { get; set; }
        public int ReorderLevel { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
    }

    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string UOM { get; set; } = string.Empty;
        public int StockQty { get; set; }
        public int ReorderLevel { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string UOM { get; set; } = string.Empty;
        public int StockQty { get; set; }
        public int ReorderLevel { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
    }
} 