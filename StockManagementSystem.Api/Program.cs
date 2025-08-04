using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StockManagementSystem.Api.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Dynamic connection string detection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    // Fallback to LocalDB if no connection string is provided
    connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=StockManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
}

// Add DbContext
builder.Services.AddDbContext<StockDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<StockDbContext>()
    .AddDefaultTokenProviders();

// Add JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "ThisIsASecretKeyForJwt";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "StockManagementSystem";
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// Add Controllers with JSON configuration to handle circular references
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.MaxDepth = 64;
    });

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockManagementSystem API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Seed roles and admin user with error handling
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await StockManagementSystem.Api.Models.DbInitializer.SeedRolesAndAdminAsync(services);
        Console.WriteLine("Database initialized successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error initializing database: {ex.Message}");
        Console.WriteLine("Please ensure SQL Server or LocalDB is installed and running.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Only use HTTPS redirection if HTTPS is configured
if (builder.Configuration["ASPNETCORE_URLS"]?.Contains("https") == true || 
    builder.Configuration["Kestrel:Endpoints:Https"] != null)
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
