using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace StockManagementSystem.Api.Models
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<StockDbContext>();

            // Seed roles
            string[] roles = new[] { "Admin", "WarehouseStaff", "Auditor" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role });
                }
            }

            // Seed default admin user
            var adminEmail = "admin@stock.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed test data
            await SeedTestDataAsync(context);
        }

        private static async Task SeedTestDataAsync(StockDbContext context)
        {
            // Seed Categories
            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Electronics", Description = "Electronic devices and components" },
                    new Category { Name = "Office Supplies", Description = "Office stationery and supplies" },
                    new Category { Name = "Furniture", Description = "Office furniture and fixtures" },
                    new Category { Name = "IT Equipment", Description = "Computers, servers, and networking equipment" },
                    new Category { Name = "Tools", Description = "Hand tools and power tools" },
                    new Category { Name = "Safety Equipment", Description = "Personal protective equipment" },
                    new Category { Name = "Cleaning Supplies", Description = "Cleaning materials and chemicals" },
                    new Category { Name = "Packaging", Description = "Packaging materials and containers" },
                    new Category { Name = "Automotive", Description = "Automotive parts and accessories" },
                    new Category { Name = "Medical Supplies", Description = "Medical equipment and supplies" }
                };
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed Products
            if (!await context.Products.AnyAsync())
            {
                var products = new List<Product>
                {
                    new Product { Name = "Laptop Dell XPS 13", SKU = "LAP-DELL-XPS13", CategoryId = 1, UOM = "PCS", StockQty = 25, ReorderLevel = 5, UnitPrice = 1299.99m, Description = "13-inch premium laptop" },
                    new Product { Name = "Wireless Mouse", SKU = "ACC-MOUSE-WL01", CategoryId = 1, UOM = "PCS", StockQty = 150, ReorderLevel = 20, UnitPrice = 29.99m, Description = "Bluetooth wireless mouse" },
                    new Product { Name = "Office Chair", SKU = "FUR-CHAIR-EX01", CategoryId = 3, UOM = "PCS", StockQty = 12, ReorderLevel = 3, UnitPrice = 299.99m, Description = "Ergonomic office chair" },
                    new Product { Name = "Network Switch", SKU = "IT-SWITCH-24P", CategoryId = 4, UOM = "PCS", StockQty = 8, ReorderLevel = 2, UnitPrice = 199.99m, Description = "24-port network switch" },
                    new Product { Name = "Screwdriver Set", SKU = "TOOL-SCREW-10P", CategoryId = 5, UOM = "SET", StockQty = 30, ReorderLevel = 5, UnitPrice = 49.99m, Description = "10-piece screwdriver set" },
                    new Product { Name = "Safety Helmet", SKU = "SAFE-HELM-YEL", CategoryId = 6, UOM = "PCS", StockQty = 45, ReorderLevel = 10, UnitPrice = 39.99m, Description = "Yellow safety helmet" },
                    new Product { Name = "All-Purpose Cleaner", SKU = "CLEAN-AP-1L", CategoryId = 7, UOM = "BOTTLE", StockQty = 60, ReorderLevel = 15, UnitPrice = 8.99m, Description = "1L all-purpose cleaner" },
                    new Product { Name = "Cardboard Boxes", SKU = "PACK-BOX-30CM", CategoryId = 8, UOM = "PCS", StockQty = 200, ReorderLevel = 50, UnitPrice = 2.99m, Description = "30cm cardboard boxes" },
                    new Product { Name = "Car Battery", SKU = "AUTO-BAT-12V", CategoryId = 9, UOM = "PCS", StockQty = 15, ReorderLevel = 3, UnitPrice = 89.99m, Description = "12V car battery" },
                    new Product { Name = "First Aid Kit", SKU = "MED-FAK-STD", CategoryId = 10, UOM = "KIT", StockQty = 20, ReorderLevel = 5, UnitPrice = 45.99m, Description = "Standard first aid kit" }
                };
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }

            // Seed GRNs
            if (!await context.GRNs.AnyAsync())
            {
                var grns = new List<GRN>
                {
                    new GRN { Supplier = "TechCorp Inc.", Date = DateTime.Now.AddDays(-30), Status = "Completed", TotalAmount = 3249.75m },
                    new GRN { Supplier = "OfficeMax Solutions", Date = DateTime.Now.AddDays(-25), Status = "Completed", TotalAmount = 899.70m },
                    new GRN { Supplier = "Furniture World", Date = DateTime.Now.AddDays(-20), Status = "Completed", TotalAmount = 1799.94m },
                    new GRN { Supplier = "Network Pro", Date = DateTime.Now.AddDays(-15), Status = "Completed", TotalAmount = 1599.92m },
                    new GRN { Supplier = "ToolMaster Ltd.", Date = DateTime.Now.AddDays(-10), Status = "Completed", TotalAmount = 1499.70m },
                    new GRN { Supplier = "Safety First Co.", Date = DateTime.Now.AddDays(-5), Status = "Completed", TotalAmount = 1799.55m },
                    new GRN { Supplier = "CleanPro Supplies", Date = DateTime.Now.AddDays(-3), Status = "Completed", TotalAmount = 539.40m },
                    new GRN { Supplier = "Packaging Plus", Date = DateTime.Now.AddDays(-2), Status = "Completed", TotalAmount = 598.00m },
                    new GRN { Supplier = "AutoParts Express", Date = DateTime.Now.AddDays(-1), Status = "Completed", TotalAmount = 1349.85m },
                    new GRN { Supplier = "Medical Supplies Co.", Date = DateTime.Now, Status = "Pending", TotalAmount = 919.80m }
                };
                await context.GRNs.AddRangeAsync(grns);
                await context.SaveChangesAsync();
            }

            // Seed GRN Items
            if (!await context.GRNItems.AnyAsync())
            {
                var grnItems = new List<GRNItem>
                {
                    new GRNItem { GRNId = 1, ProductId = 1, Qty = 2, UnitPrice = 1299.99m },
                    new GRNItem { GRNId = 1, ProductId = 2, Qty = 5, UnitPrice = 29.99m },
                    new GRNItem { GRNId = 2, ProductId = 2, Qty = 20, UnitPrice = 29.99m },
                    new GRNItem { GRNId = 2, ProductId = 7, Qty = 10, UnitPrice = 8.99m },
                    new GRNItem { GRNId = 3, ProductId = 3, Qty = 6, UnitPrice = 299.99m },
                    new GRNItem { GRNId = 4, ProductId = 4, Qty = 8, UnitPrice = 199.99m },
                    new GRNItem { GRNId = 5, ProductId = 5, Qty = 30, UnitPrice = 49.99m },
                    new GRNItem { GRNId = 6, ProductId = 6, Qty = 45, UnitPrice = 39.99m },
                    new GRNItem { GRNId = 7, ProductId = 7, Qty = 60, UnitPrice = 8.99m },
                    new GRNItem { GRNId = 8, ProductId = 8, Qty = 200, UnitPrice = 2.99m },
                    new GRNItem { GRNId = 9, ProductId = 9, Qty = 15, UnitPrice = 89.99m },
                    new GRNItem { GRNId = 10, ProductId = 10, Qty = 20, UnitPrice = 45.99m }
                };
                await context.GRNItems.AddRangeAsync(grnItems);
                await context.SaveChangesAsync();
            }

            // Seed GINs
            if (!await context.GINs.AnyAsync())
            {
                var gins = new List<GIN>
                {
                    new GIN { Recipient = "IT Department", Date = DateTime.Now.AddDays(-28), Reason = "New employee setup" },
                    new GIN { Recipient = "Marketing Team", Date = DateTime.Now.AddDays(-23), Reason = "Campaign materials" },
                    new GIN { Recipient = "HR Department", Date = DateTime.Now.AddDays(-18), Reason = "Office renovation" },
                    new GIN { Recipient = "Operations Team", Date = DateTime.Now.AddDays(-13), Reason = "Network upgrade" },
                    new GIN { Recipient = "Maintenance Crew", Date = DateTime.Now.AddDays(-8), Reason = "Equipment repair" },
                    new GIN { Recipient = "Construction Team", Date = DateTime.Now.AddDays(-3), Reason = "Safety equipment" },
                    new GIN { Recipient = "Janitorial Staff", Date = DateTime.Now.AddDays(-1), Reason = "Cleaning supplies" },
                    new GIN { Recipient = "Shipping Department", Date = DateTime.Now, Reason = "Packaging materials" },
                    new GIN { Recipient = "Fleet Management", Date = DateTime.Now, Reason = "Vehicle maintenance" },
                    new GIN { Recipient = "Medical Team", Date = DateTime.Now, Reason = "Emergency supplies" }
                };
                await context.GINs.AddRangeAsync(gins);
                await context.SaveChangesAsync();
            }

            // Seed GIN Items
            if (!await context.GINItems.AnyAsync())
            {
                var ginItems = new List<GINItem>
                {
                    new GINItem { GINId = 1, ProductId = 1, Qty = 1 },
                    new GINItem { GINId = 1, ProductId = 2, Qty = 2 },
                    new GINItem { GINId = 2, ProductId = 2, Qty = 5 },
                    new GINItem { GINId = 2, ProductId = 8, Qty = 50 },
                    new GINItem { GINId = 3, ProductId = 3, Qty = 2 },
                    new GINItem { GINId = 4, ProductId = 4, Qty = 2 },
                    new GINItem { GINId = 5, ProductId = 5, Qty = 5 },
                    new GINItem { GINId = 6, ProductId = 6, Qty = 10 },
                    new GINItem { GINId = 7, ProductId = 7, Qty = 15 },
                    new GINItem { GINId = 8, ProductId = 8, Qty = 100 },
                    new GINItem { GINId = 9, ProductId = 9, Qty = 3 },
                    new GINItem { GINId = 10, ProductId = 10, Qty = 5 }
                };
                await context.GINItems.AddRangeAsync(ginItems);
                await context.SaveChangesAsync();
            }

            // Seed Audit Logs
            if (!await context.AuditLogs.AnyAsync())
            {
                var auditLogs = new List<AuditLog>
                {
                    new AuditLog { Action = "Created", Entity = "Category", EntityId = 1, UserId = "admin@stock.com", Timestamp = DateTime.Now.AddDays(-35), Details = "Created Electronics category" },
                    new AuditLog { Action = "Created", Entity = "Product", EntityId = 1, UserId = "admin@stock.com", Timestamp = DateTime.Now.AddDays(-34), Details = "Added Laptop Dell XPS 13" },
                    new AuditLog { Action = "Created", Entity = "GRN", EntityId = 1, UserId = "admin@stock.com", Timestamp = DateTime.Now.AddDays(-30), Details = "Created GRN from TechCorp Inc." },
                    new AuditLog { Action = "Updated", Entity = "Product", EntityId = 1, UserId = "admin@stock.com", Timestamp = DateTime.Now.AddDays(-29), Details = "Updated stock quantity for Laptop" },
                    new AuditLog { Action = "Created", Entity = "GIN", EntityId = 1, UserId = "admin@stock.com", Timestamp = DateTime.Now.AddDays(-28), Details = "Issued laptop to IT Department" },
                    new AuditLog { Action = "Updated", Entity = "Product", EntityId = 1, UserId = "admin@stock.com", Timestamp = DateTime.Now.AddDays(-28), Details = "Reduced stock after GIN issue" },
                    new AuditLog { Action = "Created", Entity = "Category", EntityId = 2, UserId = "admin@stock.com", Timestamp = DateTime.Now.AddDays(-27), Details = "Created Office Supplies category" },
                    new AuditLog { Action = "Created", Entity = "Product", EntityId = 2, UserId = "admin@stock.com", Timestamp = DateTime.Now.AddDays(-26), Details = "Added Wireless Mouse" },
                    new AuditLog { Action = "Updated", Entity = "GRN", EntityId = 1, UserId = "admin@stock.com", Timestamp = DateTime.Now.AddDays(-25), Details = "Marked GRN as completed" },
                    new AuditLog { Action = "Created", Entity = "GIN", EntityId = 2, UserId = "admin@stock.com", Timestamp = DateTime.Now.AddDays(-23), Details = "Issued supplies to Marketing Team" }
                };
                await context.AuditLogs.AddRangeAsync(auditLogs);
                await context.SaveChangesAsync();
            }
        }
    }
} 