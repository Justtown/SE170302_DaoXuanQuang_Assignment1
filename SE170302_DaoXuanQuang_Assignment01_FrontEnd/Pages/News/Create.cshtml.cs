using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs;
using System.Net.Http.Json;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.Pages.News
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CreateModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [BindProperty]
        public NewsArticleDto News { get; set; } = new();

        [BindProperty]
        public List<int> SelectedTagIds { get; set; } = new();

        public List<CategoryDto> Categories { get; set; } = new();
        public List<TagDto> AllTags { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("ODataApi");
            var catRes = await client.GetFromJsonAsync<ODataResponse<CategoryDto>>("odata/Categories");
            Categories = catRes?.Value ?? new();
            var tagRes = await client.GetFromJsonAsync<ODataResponse<TagDto>>("odata/Tags");
            AllTags = tagRes?.Value ?? new();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");

                var accountId = HttpContext.Session.GetString("AccountId");
                News.CreatedById = short.Parse(accountId ?? "1");
                News.TagIds = SelectedTagIds;

                var response = await client.PostAsJsonAsync("odata/NewsArticles", News);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Article created successfully.";
                    return RedirectToPage("./Index");
                }
                ErrorMessage = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex) { ErrorMessage = $"Create failed: {ex.Message}"; }
            return Page();
        }
    }
}