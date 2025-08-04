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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for all decimal properties
            modelBuilder.Entity<GRN>()
                .Property(g => g.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<GRNItem>()
                .Property(gi => gi.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,2)");

            // Add indexes for better performance
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CategoryId);

            modelBuilder.Entity<GRN>()
                .HasIndex(g => g.Date);

            modelBuilder.Entity<GIN>()
                .HasIndex(g => g.Date);

            modelBuilder.Entity<AuditLog>()
                .HasIndex(a => a.Timestamp);

            // Configure relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GRNItem>()
                .HasOne(gi => gi.GRN)
                .WithMany(g => g.GRNItems)
                .HasForeignKey(gi => gi.GRNId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GRNItem>()
                .HasOne(gi => gi.Product)
                .WithMany()
                .HasForeignKey(gi => gi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GINItem>()
                .HasOne(gi => gi.GIN)
                .WithMany(g => g.GINItems)
                .HasForeignKey(gi => gi.GINId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GINItem>()
                .HasOne(gi => gi.Product)
                .WithMany()
                .HasForeignKey(gi => gi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 