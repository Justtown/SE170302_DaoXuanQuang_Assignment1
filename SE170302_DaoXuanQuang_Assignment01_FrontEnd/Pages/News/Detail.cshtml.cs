using Microsoft.AspNetCore.Mvc.RazorPages;
using SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs;
using System.Net.Http.Json;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.Pages.News
{
    public class DetailModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DetailModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public NewsArticleDto News { get; set; } = new();

        public async Task OnGetAsync(string id)
        {
            var client = _httpClientFactory.CreateClient("ODataApi");
            News = await client.GetFromJsonAsync<NewsArticleDto>($"odata/NewsArticles('{id}')") ?? new();
        }
    }
}