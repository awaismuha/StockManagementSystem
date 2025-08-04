using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Api.Models;
using StockManagementSystem.Api.DTOs;

namespace StockManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly StockDbContext _context;
        public ProductController(StockDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            
            var productDtos = products.Select(product => new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                SKU = product.SKU,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "",
                UOM = product.UOM,
                StockQty = product.StockQty,
                ReorderLevel = product.ReorderLevel,
                UnitPrice = product.UnitPrice,
                Description = product.Description
            }).ToList();

            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
            if (product == null) return NotFound();
            
            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                SKU = product.SKU,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? "",
                UOM = product.UOM,
                StockQty = product.StockQty,
                ReorderLevel = product.ReorderLevel,
                UnitPrice = product.UnitPrice,
                Description = product.Description
            };
            
            return Ok(productDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,WarehouseStaff")]
        public async Task<IActionResult> Create(CreateProductDto createProductDto)
        {
            var product = new Product
            {
                Name = createProductDto.Name,
                SKU = createProductDto.SKU,
                CategoryId = createProductDto.CategoryId,
                UOM = createProductDto.UOM,
                StockQty = createProductDto.StockQty,
                ReorderLevel = createProductDto.ReorderLevel,
                UnitPrice = createProductDto.UnitPrice,
                Description = createProductDto.Description
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,WarehouseStaff")]
        public async Task<IActionResult> Update(int id, UpdateProductDto updateProductDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            
            product.Name = updateProductDto.Name;
            product.SKU = updateProductDto.SKU;
            product.CategoryId = updateProductDto.CategoryId;
            product.UOM = updateProductDto.UOM;
            product.StockQty = updateProductDto.StockQty;
            product.ReorderLevel = updateProductDto.ReorderLevel;
            product.UnitPrice = updateProductDto.UnitPrice;
            product.Description = updateProductDto.Description;
            
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 