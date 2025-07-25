using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Web.Models;
using System.Text;
using System.Text.Json;

namespace StockManagementSystem.Web.Controllers
{
    [Authorize]
    public class GINController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public GINController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/GIN";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var gins = JsonSerializer.Deserialize<List<GINViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(gins);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new GINViewModel { Date = DateTime.Now });
        }

        [HttpPost]
        public async Task<IActionResult> Create(GINViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/GIN";
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Create failed.";
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + $"/api/GIN/confirm/{id}";
            var response = await client.PostAsync(apiUrl, null);
            return RedirectToAction("Index");
        }
    }
} 