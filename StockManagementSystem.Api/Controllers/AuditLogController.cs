using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Api.Models;

namespace StockManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Auditor")]
    public class AuditLogController : ControllerBase
    {
        private readonly StockDbContext _context;
        public AuditLogController(StockDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var logs = await _context.AuditLogs.OrderByDescending(l => l.Timestamp).ToListAsync();
            return Ok(logs);
        }
    }
} 