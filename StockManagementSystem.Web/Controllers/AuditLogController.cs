using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Web.Models;
using System.Text.Json;

namespace StockManagementSystem.Web.Controllers
{
    [Authorize(Roles = "Admin,Auditor")]
    public class AuditLogController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public AuditLogController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/AuditLog";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var logs = JsonSerializer.Deserialize<List<AuditLogViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(logs);
        }
    }
} 