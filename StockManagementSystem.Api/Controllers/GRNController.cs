using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Api.Models;

namespace StockManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GRNController : ControllerBase
    {
        private readonly StockDbContext _context;
        public GRNController(StockDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.GRNs.Include(g => g.GRNItems).ThenInclude(i => i.Product).ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var grn = await _context.GRNs.Include(g => g.GRNItems).ThenInclude(i => i.Product).FirstOrDefaultAsync(g => g.GRNId == id);
            if (grn == null) return NotFound();
            return Ok(grn);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,WarehouseStaff")]
        public async Task<IActionResult> Create(GRN grn)
        {
            grn.Status = "Pending";
            _context.GRNs.Add(grn);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = grn.GRNId }, grn);
        }

        [HttpPost("approve/{id}")]
        [Authorize(Roles = "Admin,WarehouseStaff")]
        public async Task<IActionResult> Approve(int id)
        {
            var grn = await _context.GRNs.Include(g => g.GRNItems).FirstOrDefaultAsync(g => g.GRNId == id);
            if (grn == null || grn.Status == "Approved") return BadRequest();
            grn.Status = "Approved";
            foreach (var item in grn.GRNItems ?? new List<GRNItem>())
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.StockQty += item.Qty;
                }
            }
            await _context.SaveChangesAsync();
            // Audit log
            _context.AuditLogs.Add(new AuditLog {
                Action = "GRN Approved",
                Entity = "GRN",
                EntityId = grn.GRNId,
                UserId = User.Identity?.Name,
                Timestamp = DateTime.UtcNow,
                Details = $"GRN {grn.GRNId} approved."
            });
            await _context.SaveChangesAsync();
            return Ok(grn);
        }
    }
} 