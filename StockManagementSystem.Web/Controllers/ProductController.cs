using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Web.Models;
using System.Text;
using System.Text.Json;

namespace StockManagementSystem.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public ProductController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/Product";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<ProductViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/Product";
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Create failed.";
                await PopulateCategories();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + $"/api/Product/{id}";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var product = JsonSerializer.Deserialize<ProductViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            await PopulateCategories();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + $"/api/Product/{model.ProductId}";
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Edit failed.";
                await PopulateCategories();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + $"/api/Product/{id}";
            var response = await client.DeleteAsync(apiUrl);
            return RedirectToAction("Index");
        }

        private async Task PopulateCategories()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/Category";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<CategoryViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            ViewBag.Categories = categories ?? new List<CategoryViewModel>();
        }
    }
} 