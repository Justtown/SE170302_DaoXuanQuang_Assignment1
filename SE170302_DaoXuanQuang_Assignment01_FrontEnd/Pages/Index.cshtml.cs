using Microsoft.AspNetCore.Mvc.RazorPages;
using SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs;
using System.Net.Http.Json;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.Pages
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
                // OData filter: chỉ lấy bài có NewsStatus = true, sắp xếp theo CreatedDate giảm dần
                var url = "odata/NewsArticles?$filter=NewsStatus eq true&$orderby=CreatedDate desc";
                var response = await client.GetFromJsonAsync<ODataResponse<NewsArticleDto>>(url);
                if (response?.Value != null) NewsList = response.Value;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Unable to load news: {ex.Message}";
            }
        }
    }
}