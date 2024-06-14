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
    public class CustomersMvcController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomersMvcController(IHttpClientFactory httpClientFactory)
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
            HttpResponseMessage response = await client.GetAsync("http://localhost:5091/api/customers");
            if (response.IsSuccessStatusCode)
            {
                var customers = await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();
                if (!string.IsNullOrEmpty(searchString))
                {
                    customers = customers.Where(d => d.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase));
                }
                return View(customers);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // Xử lý khi không có quyền truy cập
                return RedirectToAction("Login", "DangNhap");
            }

            // Xử lý các lỗi khác
            return View(new List<Customer>());
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = _httpClientFactory.CreateClient();
            AddAuthorizationHeader(client);

            HttpResponseMessage response = await client.GetAsync($"http://localhost:5091/api/customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var customer = await response.Content.ReadFromJsonAsync<Customer>();
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
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
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                AddAuthorizationHeader(client);

                HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:5091/api/customers", customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "DangNhap");
                }
            }
            return View(customer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            AddAuthorizationHeader(client);

            HttpResponseMessage response = await client.GetAsync($"http://localhost:5091/api/customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var customer = await response.Content.ReadFromJsonAsync<Customer>();
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "DangNhap");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                AddAuthorizationHeader(client);

                HttpResponseMessage response = await client.PutAsJsonAsync($"http://localhost:5091/api/customers/{id}", customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "DangNhap");
                }
            }
            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            AddAuthorizationHeader(client);

            HttpResponseMessage response = await client.GetAsync($"http://localhost:5091/api/customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var customer = await response.Content.ReadFromJsonAsync<Customer>();
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
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

            HttpResponseMessage response = await client.DeleteAsync($"http://localhost:5091/api/customers/{id}");
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
