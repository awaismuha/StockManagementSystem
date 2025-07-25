using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Web.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace StockManagementSystem.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public CategoryController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/Category";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<List<CategoryViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/Category";
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Create failed.";
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + $"/api/Category/{id}";
            var response = await client.GetAsync(apiUrl);
            var json = await response.Content.ReadAsStringAsync();
            var category = JsonSerializer.Deserialize<CategoryViewModel>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + $"/api/Category/{model.CategoryId}";
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PutAsync(apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Edit failed.";
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + $"/api/Category/{id}";
            var response = await client.DeleteAsync(apiUrl);
            return RedirectToAction("Index");
        }
    }
} 