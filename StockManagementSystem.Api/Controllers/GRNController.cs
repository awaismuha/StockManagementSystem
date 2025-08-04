using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Api.Models;
using StockManagementSystem.Api.DTOs;

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
            var grns = await _context.GRNs
                .Include(g => g.GRNItems)
                .ThenInclude(i => i.Product)
                .ToListAsync();

            var grnDtos = grns.Select(grn => new GRNDto
            {
                GRNId = grn.GRNId,
                Supplier = grn.Supplier,
                Date = grn.Date,
                Status = grn.Status,
                TotalAmount = grn.TotalAmount,
                GRNItems = grn.GRNItems?.Select(item => new GRNItemDto
                {
                    GRNItemId = item.GRNItemId,
                    GRNId = item.GRNId,
                    ProductId = item.ProductId,
                    ProductName = item.Product?.Name ?? "",
                    ProductSKU = item.Product?.SKU ?? "",
                    Qty = item.Qty,
                    UnitPrice = item.UnitPrice
                }).ToList()
            }).ToList();

            return Ok(grnDtos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var grn = await _context.GRNs
                .Include(g => g.GRNItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(g => g.GRNId == id);
            
            if (grn == null) return NotFound();
            
            var grnDto = new GRNDto
            {
                GRNId = grn.GRNId,
                Supplier = grn.Supplier,
                Date = grn.Date,
                Status = grn.Status,
                TotalAmount = grn.TotalAmount,
                GRNItems = grn.GRNItems?.Select(item => new GRNItemDto
                {
                    GRNItemId = item.GRNItemId,
                    GRNId = item.GRNId,
                    ProductId = item.ProductId,
                    ProductName = item.Product?.Name ?? "",
                    ProductSKU = item.Product?.SKU ?? "",
                    Qty = item.Qty,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };
            
            return Ok(grnDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,WarehouseStaff")]
        public async Task<IActionResult> Create(CreateGRNDto createGrnDto)
        {
            var grn = new GRN
            {
                Supplier = createGrnDto.Supplier,
                Date = createGrnDto.Date,
                Status = createGrnDto.Status,
                GRNItems = createGrnDto.GRNItems?.Select(item => new GRNItem
                {
                    ProductId = item.ProductId,
                    Qty = item.Qty,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

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