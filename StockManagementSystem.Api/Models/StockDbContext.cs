using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StockManagementSystem.Api.Models
{
    public class StockDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<GRN> GRNs { get; set; }
        public DbSet<GRNItem> GRNItems { get; set; }
        public DbSet<GIN> GINs { get; set; }
        public DbSet<GINItem> GINItems { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
} 