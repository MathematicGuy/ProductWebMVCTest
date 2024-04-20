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

        // https://heval1st-001-site1.anytempurl.com/api/Hang
        // declare global API

        public async Task<IActionResult> ViewHang()
        {
            var httpClient = _httpClientFactory.CreateClient();
        
            var response = httpClient.GetAsync("https://heval1st-001-site1.anytempurl.com/api/Hang/GetAllHang").Result;
            Console.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                var hangHoa = await response.Content.ReadFromJsonAsync<IEnumerable<HangHoa>>();
                return View(hangHoa);
            }


            //ViewBag.ErrorMessage = "An error occurred. Please try again.";
            return View();
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
            var response = await httpClient.PostAsync("https://heval1st-001-site1.anytempurl.com/api/Hang/CreateHang", content);

            if (response.IsSuccessStatusCode) 
            {
                return RedirectToAction("ViewHang"); // Redirect to the list view after create
            }
            else
            {
                // Handle errors
                ViewBag.ErrorMessage = "Input value is False, Please re-enter";
                return View(null);
            }
        }


        [HttpGet]
        public async Task<IActionResult> ViewHangById(int? Id)
        {
            if (!Id.HasValue)
            {
                ViewBag.ErrorMessage = "Input value is False";
                return View(); // Display initial view without data
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://heval1st-001-site1.anytempurl.com/api/Hang/HangById{Id.Value}");

            if (response.IsSuccessStatusCode)
            {
                var hangHoa = await response.Content.ReadFromJsonAsync<HangHoa>();
                return View(hangHoa);
            }
            else
            {
                // Handle error (e.g., View("Error"))
                ViewBag.ErrorMessage = "Id Not Exist, Please enter a exist Id";
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
            var response = await httpClient.GetAsync($"https://heval1st-001-site1.anytempurl.com/api/Hang/HangById{Id}");

            if (response.IsSuccessStatusCode)
            {
                var hangHoa = await response.Content.ReadFromJsonAsync<HangHoa>();
                return View(hangHoa);
            }
            else
            {
                // Handle error (e.g., View("Error"))
                return RedirectToAction("ViewHang");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHang(HangHoa hangHoa, int? Id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            Console.Write(hangHoa);
            // Serialize data appropriately for your API
            var content = new StringContent(JsonConvert.SerializeObject(hangHoa), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://heval1st-001-site1.anytempurl.com/api/Hang/UpdateHang{Id}", content);

            if (response.IsSuccessStatusCode)
            {
                // Success! Redirect or display a success message
                return RedirectToAction("ViewHang");  // Example redirect
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                if (errorResponse != null)
                {
                    // Add error messages to ViewData or ModelState
                    ViewData["ErrorMessages"] = errorResponse.Details;
                }
                else
                {
                    // Handle unexpected error
                    ViewData["ErrorMessage"] = "An error occurred while updating.";
                }
                return View(hangHoa); // Pass the model back so user input is retained
            }
        }




        [HttpPost] // Important: Use HttpPost for deletions
        public async Task<IActionResult> DeleteHang(int? Id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"https://heval1st-001-site1.anytempurl.com/api/Hang/DelelteHang{Id}");

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
