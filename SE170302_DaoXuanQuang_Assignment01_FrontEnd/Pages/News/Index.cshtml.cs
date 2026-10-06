using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs;
using System.Net.Http.Json;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<NewsArticleDto> NewsList { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var url = "odata/NewsArticles?$orderby=CreatedDate desc&$expand=Category,CreatedBy";
                var response = await client.GetFromJsonAsync<ODataResponse<NewsArticleDto>>(url);
                if (response?.Value != null) NewsList = response.Value;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Unable to load news: {ex.Message}";
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var response = await client.DeleteAsync($"odata/NewsArticles('{id}')");
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Article deleted successfully.";
                }
                else
                {
                    TempData["Error"] = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Delete failed: {ex.Message}";
            }
            return RedirectToPage();
        }
    }
}