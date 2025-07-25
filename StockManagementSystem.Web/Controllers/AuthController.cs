using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace StockManagementSystem.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/Auth/login";
            var content = new StringContent(JsonSerializer.Serialize(new { Email = email, Password = password }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Invalid login.";
                return View();
            }
            var json = await response.Content.ReadAsStringAsync();
            var token = JsonDocument.Parse(json).RootElement.GetProperty("token").GetString();
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var claims = jwt.Claims.ToList();
            // Map 'role' claims to ClaimTypes.Role for cookie authentication
            var mappedClaims = claims.Select(c =>
                c.Type == "role"
                    ? new Claim(ClaimTypes.Role, c.Value)
                    : c
            ).ToList();
            // Ensure ClaimTypes.Name is present for the cookie
            if (!mappedClaims.Any(c => c.Type == ClaimTypes.Name))
            {
                // Try to use email or sub as the name
                var emailClaim = claims.FirstOrDefault(c => c.Type == "email")?.Value;
                var subClaim = claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                mappedClaims.Add(new Claim(ClaimTypes.Name, emailClaim ?? subClaim ?? "User"));
            }
            var identity = new ClaimsIdentity(mappedClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            // Debug
            Console.WriteLine("User signed in: " + identity.Name);
            HttpContext.Session.SetString("JWToken", token!);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string role)
        {
            var client = _httpClientFactory.CreateClient();
            var apiUrl = _config["ApiBaseUrl"] + "/api/Auth/register";
            var content = new StringContent(JsonSerializer.Serialize(new { Email = email, Password = password, Role = role }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Registration failed.";
                return View();
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Login");
        }
    }
} 