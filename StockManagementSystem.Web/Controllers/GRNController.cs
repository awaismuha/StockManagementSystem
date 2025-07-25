using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Web.Models;
using System.Text;
using System.Text.Json;

namespace StockManagementSystem.Web.Controllers
{
    [Authorize]
    public class GRNController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public GRNController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/GRN";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var grns = JsonSerializer.Deserialize<List<GRNViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(grns);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new GRNViewModel { Date = DateTime.Now });
        }

        [HttpPost]
        public async Task<IActionResult> Create(GRNViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/GRN";
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
        public async Task<IActionResult> Approve(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + $"/api/GRN/approve/{id}";
            var response = await client.PostAsync(apiUrl, null);
            return RedirectToAction("Index");
        }
    }
} 