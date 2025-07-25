using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Api.Models;

namespace StockManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StockReportController : ControllerBase
    {
        private readonly StockDbContext _context;
        public StockReportController(StockDbContext context)
        {
            _context = context;
        }

        [HttpGet("realtime")]
        public async Task<IActionResult> GetRealTimeStock()
        {
            var stock = await _context.Products.Include(p => p.Category)
                .Select(p => new {
                    p.ProductId,
                    p.Name,
                    p.SKU,
                    Category = p.Category != null ? p.Category.Name : null,
                    p.UOM,
                    p.StockQty,
                    p.ReorderLevel
                }).ToListAsync();
            return Ok(stock);
        }

        [HttpGet("lowstock")]
        public async Task<IActionResult> GetLowStockAlerts()
        {
            var lowStock = await _context.Products.Where(p => p.StockQty < p.ReorderLevel).ToListAsync();
            return Ok(lowStock);
        }

        [HttpGet("movements")]
        public async Task<IActionResult> GetStockMovements()
        {
            var grns = await _context.GRNs.Include(g => g.GRNItems).ToListAsync();
            var gins = await _context.GINs.Include(g => g.GINItems).ToListAsync();
            return Ok(new { GRNs = grns, GINs = gins });
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var grnCount = await _context.GRNs.CountAsync();
            var ginCount = await _context.GINs.CountAsync();
            return Ok(new { GRNCount = grnCount, GINCount = ginCount });
        }

        [HttpGet("valuation")]
        public async Task<IActionResult> GetInventoryValuation()
        {
            var products = await _context.Products.ToListAsync();
            var grnItems = await _context.GRNItems.ToListAsync();
            var valuation = products.Sum(p =>
            {
                var lastGrn = grnItems.Where(i => i.ProductId == p.ProductId).OrderByDescending(i => i.GRNItemId).FirstOrDefault();
                var unitPrice = lastGrn?.UnitPrice ?? 0;
                return p.StockQty * unitPrice;
            });
            return Ok(new { TotalValuation = valuation });
        }
    }
} 