using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Web.Models;
using System.Text.Json;

namespace StockManagementSystem.Web.Controllers
{
    [Authorize]
    public class StockReportController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public StockReportController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IActionResult> RealTime()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/StockReport/realtime";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<List<StockItemViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(items);
        }

        public async Task<IActionResult> LowStock()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/StockReport/lowstock";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var items = JsonSerializer.Deserialize<List<StockItemViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(items);
        }

        public async Task<IActionResult> Movements()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/StockReport/movements";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var movements = JsonSerializer.Deserialize<StockMovementViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(movements);
        }

        public async Task<IActionResult> Summary()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/StockReport/summary";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var summary = JsonDocument.Parse(json).RootElement;
            ViewBag.GRNCount = summary.GetProperty("GRNCount").GetInt32();
            ViewBag.GINCount = summary.GetProperty("GINCount").GetInt32();
            return View();
        }

        public async Task<IActionResult> Valuation()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/StockReport/valuation";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var valuation = JsonDocument.Parse(json).RootElement.GetProperty("TotalValuation").GetDecimal();
            ViewBag.TotalValuation = valuation;
            return View();
        }
    }
} 