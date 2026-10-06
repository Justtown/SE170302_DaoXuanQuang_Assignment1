using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs;
using System.Net.Http.Json;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<CategoryDto> Categories { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var url = "odata/Categories";
                var response = await client.GetFromJsonAsync<ODataResponse<CategoryDto>>(url);
                if (response?.Value != null) Categories = response.Value;

                // DEBUG: In ra console
                Console.WriteLine($"Categories loaded: {Categories.Count}");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Unable to load categories: {ex.Message}";
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(short id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var response = await client.DeleteAsync($"odata/Categories({id})");
                if (response.IsSuccessStatusCode)
                    TempData["Success"] = "Category deleted successfully.";
                else
                    TempData["Error"] = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Delete failed: {ex.Message}";
            }
            return RedirectToPage();
        }
    }
}