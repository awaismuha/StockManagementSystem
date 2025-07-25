using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Api.Models;

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
            return Ok(await _context.GINs.Include(g => g.GINItems).ThenInclude(i => i.Product).ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var gin = await _context.GINs.Include(g => g.GINItems).ThenInclude(i => i.Product).FirstOrDefaultAsync(g => g.GINId == id);
            if (gin == null) return NotFound();
            return Ok(gin);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,WarehouseStaff")]
        public async Task<IActionResult> Create(GIN gin)
        {
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