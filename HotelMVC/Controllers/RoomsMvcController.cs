using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using HotelMVC.Models;
using System.Net.Http.Headers;

namespace HotelMVC.Controllers
{
    public class RoomsMvcController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RoomsMvcController> _logger;

        public RoomsMvcController(HttpClient httpClient, ILogger<RoomsMvcController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        private void AddAuthorizationHeader()
        {
            var token = HttpContext.Session.GetString("AccessToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IActionResult> TrangChu(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            IEnumerable<Room> rooms;

            try
            {
                AddAuthorizationHeader();
                rooms = await _httpClient.GetFromJsonAsync<IEnumerable<Room>>("http://localhost:5091/api/rooms");

                if (!string.IsNullOrEmpty(searchString))
                {
                    rooms = rooms.Where(r => r.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase));
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "An error occurred while fetching rooms.");
                rooms = Enumerable.Empty<Room>();
            }

            return View(rooms);
        }

        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            IEnumerable<Room> rooms;

            try
            {
                AddAuthorizationHeader();
                rooms = await _httpClient.GetFromJsonAsync<IEnumerable<Room>>("http://localhost:5091/api/rooms");

                if (!string.IsNullOrEmpty(searchString))
                {
                    rooms = rooms.Where(r => r.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase));
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "An error occurred while fetching rooms.");
                rooms = Enumerable.Empty<Room>();
            }

            return View(rooms);
        }

        public async Task<IActionResult> Details(int id)
        {
            Room room;
            try
            {
                AddAuthorizationHeader();
                room = await _httpClient.GetFromJsonAsync<Room>($"http://localhost:5091/api/rooms/{id}");
                if (room == null)
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "An error occurred while fetching room details.");
                return NotFound();
            }
            return View(room);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AddAuthorizationHeader();
                    var response = await _httpClient.PostAsJsonAsync("http://localhost:5091/api/rooms", room);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (HttpRequestException e)
                {
                    _logger.LogError(e, "An error occurred while creating the room.");
                }
            }
            return View(room);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Room room;
            try
            {
                AddAuthorizationHeader();
                room = await _httpClient.GetFromJsonAsync<Room>($"http://localhost:5091/api/rooms/{id}");
                if (room == null)
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "An error occurred while fetching room details.");
                return NotFound();
            }
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room)
        {
            if (id != room.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AddAuthorizationHeader();
                    var response = await _httpClient.PutAsJsonAsync($"http://localhost:5091/api/rooms/{id}", room);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (HttpRequestException e)
                {
                    _logger.LogError(e, "An error occurred while updating the room.");
                }
            }
            return View(room);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Room room;
            try
            {
                AddAuthorizationHeader();
                room = await _httpClient.GetFromJsonAsync<Room>($"http://localhost:5091/api/rooms/{id}");
                if (room == null)
                {
                    return NotFound();
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "An error occurred while fetching room details.");
                return NotFound();
            }
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                AddAuthorizationHeader();
                var response = await _httpClient.DeleteAsync($"http://localhost:5091/api/rooms/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "An error occurred while deleting the room.");
            }
            return View();
        }
    }
}
