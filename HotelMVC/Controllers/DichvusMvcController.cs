using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HotelMVC.Models;
using System.Net.Http.Headers;

namespace HotelMVC.Controllers
{
    public class DichvusMvcController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DichvusMvcController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private void AddAuthorizationHeader(HttpClient client)
        {
            var token = HttpContext.Session.GetString("AccessToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var client = _httpClientFactory.CreateClient();
            AddAuthorizationHeader(client);

            ViewData["CurrentFilter"] = searchString;

            // Gọi API
            HttpResponseMessage response = await client.GetAsync("http://localhost:5091/api/dichvu");
            if (response.IsSuccessStatusCode)
            {
                var dichvus = await response.Content.ReadFromJsonAsync<IEnumerable<Dichvu>>();
                if (!string.IsNullOrEmpty(searchString))
                {
                    dichvus = dichvus.Where(d => d.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase));
                }
                return View(dichvus);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // Xử lý khi không có quyền truy cập
                return RedirectToAction("Login", "DangNhap");
            }

            // Xử lý các lỗi khác
            return View(new List<Dichvu>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient();
            AddAuthorizationHeader(client);

            HttpResponseMessage response = await client.GetAsync($"http://localhost:5091/api/dichvu/{id}");
            if (response.IsSuccessStatusCode)
            {
                var dichvu = await response.Content.ReadFromJsonAsync<Dichvu>();
                if (dichvu == null)
                {
                    return NotFound();
                }
                return View(dichvu);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "DangNhap");
            }

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dichvu dichvu)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                AddAuthorizationHeader(client);

                HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:5091/api/dichvu", dichvu);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "DangNhap");
                }
            }
            return View(dichvu);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            AddAuthorizationHeader(client);

            HttpResponseMessage response = await client.GetAsync($"http://localhost:5091/api/dichvu/{id}");
            if (response.IsSuccessStatusCode)
            {
                var dichvu = await response.Content.ReadFromJsonAsync<Dichvu>();
                if (dichvu == null)
                {
                    return NotFound();
                }
                return View(dichvu);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "DangNhap");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Dichvu dichvu)
        {
            if (id != dichvu.DichvuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                AddAuthorizationHeader(client);

                HttpResponseMessage response = await client.PutAsJsonAsync($"http://localhost:5091/api/dichvu/{id}", dichvu);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "DangNhap");
                }
            }
            return View(dichvu);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            AddAuthorizationHeader(client);

            HttpResponseMessage response = await client.GetAsync($"http://localhost:5091/api/dichvu/{id}");
            if (response.IsSuccessStatusCode)
            {
                var dichvu = await response.Content.ReadFromJsonAsync<Dichvu>();
                if (dichvu == null)
                {
                    return NotFound();
                }
                return View(dichvu);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "DangNhap");
            }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = _httpClientFactory.CreateClient();
            AddAuthorizationHeader(client);

            HttpResponseMessage response = await client.DeleteAsync($"http://localhost:5091/api/dichvu/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "DangNhap");
            }

            return View();
        }
    }
}
