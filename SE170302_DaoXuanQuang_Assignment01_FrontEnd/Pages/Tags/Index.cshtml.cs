using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs;
using System.Net.Http.Json;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ✅ PROPERTY BỊ THIẾU - BẮT BUỘC PHẢI CÓ
        public List<TagDto> Tags { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var response = await client.GetFromJsonAsync<ODataResponse<TagDto>>("odata/Tags");
                if (response?.Value != null) Tags = response.Value;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Unable to load tags: {ex.Message}";
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var response = await client.DeleteAsync($"odata/Tags({id})");
                if (response.IsSuccessStatusCode)
                    TempData["Success"] = "Tag deleted successfully.";
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