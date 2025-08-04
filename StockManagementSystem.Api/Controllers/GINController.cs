using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Api.Models;
using StockManagementSystem.Api.DTOs;

namespace StockManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GINController : ControllerBase
    {
        private readonly StockDbContext _context;
        public GINController(StockDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var gins = await _context.GINs
                .Include(g => g.GINItems)
                .ThenInclude(i => i.Product)
                .ToListAsync();

            var ginDtos = gins.Select(gin => new GINDto
            {
                GINId = gin.GINId,
                Recipient = gin.Recipient,
                Date = gin.Date,
                Reason = gin.Reason,
                GINItems = gin.GINItems?.Select(item => new GINItemDto
                {
                    GINItemId = item.GINItemId,
                    GINId = item.GINId,
                    ProductId = item.ProductId,
                    ProductName = item.Product?.Name ?? "",
                    ProductSKU = item.Product?.SKU ?? "",
                    Qty = item.Qty
                }).ToList()
            }).ToList();

            return Ok(ginDtos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var gin = await _context.GINs
                .Include(g => g.GINItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(g => g.GINId == id);
            
            if (gin == null) return NotFound();
            
            var ginDto = new GINDto
            {
                GINId = gin.GINId,
                Recipient = gin.Recipient,
                Date = gin.Date,
                Reason = gin.Reason,
                GINItems = gin.GINItems?.Select(item => new GINItemDto
                {
                    GINItemId = item.GINItemId,
                    GINId = item.GINId,
                    ProductId = item.ProductId,
                    ProductName = item.Product?.Name ?? "",
                    ProductSKU = item.Product?.SKU ?? "",
                    Qty = item.Qty
                }).ToList()
            };
            
            return Ok(ginDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,WarehouseStaff")]
        public async Task<IActionResult> Create(CreateGINDto createGinDto)
        {
            var gin = new GIN
            {
                Recipient = createGinDto.Recipient,
                Date = createGinDto.Date,
                Reason = createGinDto.Reason,
                GINItems = createGinDto.GINItems?.Select(item => new GINItem
                {
                    ProductId = item.ProductId,
                    Qty = item.Qty
                }).ToList()
            };

            _context.GINs.Add(gin);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(Get), new { id = gin.GINId }, gin);
        }

        [HttpPost("confirm/{id}")]
        [Authorize(Roles = "Admin,WarehouseStaff")]
        public async Task<IActionResult> Confirm(int id)
        {
            var gin = await _context.GINs.Include(g => g.GINItems).FirstOrDefaultAsync(g => g.GINId == id);
            if (gin == null) return BadRequest();
            foreach (var item in gin.GINItems ?? new List<GINItem>())
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.StockQty -= item.Qty;
                }
            }
            await _context.SaveChangesAsync();
            // Audit log
            _context.AuditLogs.Add(new AuditLog {
                Action = "GIN Confirmed",
                Entity = "GIN",
                EntityId = gin.GINId,
                UserId = User.Identity?.Name,
                Timestamp = DateTime.UtcNow,
                Details = $"GIN {gin.GINId} confirmed."
            });
            await _context.SaveChangesAsync();
            return Ok(gin);
        }
    }
} 