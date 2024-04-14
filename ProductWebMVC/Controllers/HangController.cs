using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductWebMVC.Models;
using System.Diagnostics;
using System.Text;

namespace ProductWebMVC.Controllers
{
    public class HangController : Controller
    {
private readonly IHttpClientFactory _httpClientFactory;

    public HangController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }


        public async Task<IActionResult> ViewHang()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = httpClient.GetAsync("https://localhost:7116/api/Hang/GetAllHang").Result;
            Console.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                var hangHoa = await response.Content.ReadFromJsonAsync<IEnumerable<HangHoa>>();
                return View(hangHoa);
            }
            return View(null);
        }

        [HttpGet]
        public IActionResult CreateHang()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateHang(HangHoa hangHoa)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(hangHoa), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:7116/api/Hang/CreateHang", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ViewHang"); // Redirect to the list view after create
            }
            else
            {
                return View(null);
            }
        }

 

        [HttpGet]
        public async Task<IActionResult> ViewHangById(int? Id)
        {
            if (!Id.HasValue)
            {
                return View(); // Display initial view without data
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7116/api/Hang/HangById{Id.Value}");

            if (response.IsSuccessStatusCode)
            {
                var hangHoa = await response.Content.ReadFromJsonAsync<HangHoa>();
                return View(hangHoa);
            }
            else
            {
                // Handle error (e.g., View("Error"))
                return View(null);
            }
        }



        [HttpGet]
        public async Task<IActionResult> UpdateHang(int? Id)
        {
            if (!Id.HasValue)
            {
                return View(); // Display initial view without data
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7116/api/Hang/HangById{Id}");

            if (response.IsSuccessStatusCode)
            {
                var hangHoa = await response.Content.ReadFromJsonAsync<HangHoa>();
                return View(hangHoa);
            }
            else
            {
                // Handle error (e.g., View("Error"))
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHang(HangHoa hangHoa, int? Id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            Console.Write(hangHoa);
            // Serialize data appropriately for your API
            var content = new StringContent(JsonConvert.SerializeObject(hangHoa), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://localhost:7116/api/Hang/UpdateHang{Id}", content);

            if (response.IsSuccessStatusCode)
            {
                // Success! Redirect or display a success message
                return RedirectToAction("Index");  // Example redirect
            }
            else
            {
                // Handle the error
                return View("Error"); // Example error handling
            }
        }

        [HttpPost] // Important: Use HttpPost for deletions
        public async Task<IActionResult> DeleteHang(int? Id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"https://localhost:7116/api/Hang/DelelteHang{Id}");

            if (response.IsSuccessStatusCode)
            {
                // Consider returning a success view or redirecting
                return RedirectToAction("ViewHang");  // Redirect to a list view 
            }
            else
            {
                // Handle error, e.g., display an error message
                return View(null);
            }

        }
    
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
