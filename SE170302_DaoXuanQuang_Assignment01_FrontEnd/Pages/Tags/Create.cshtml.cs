using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs;
using System.Net.Http.Json;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.Pages.Tags
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CreateModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        [BindProperty]
        public TagDto Tag { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var response = await client.PostAsJsonAsync("odata/Tags", Tag);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Tag created successfully.";
                    return RedirectToPage("./Index");
                }
                ErrorMessage = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex) { ErrorMessage = $"Create failed: {ex.Message}"; }
            return Page();
        }
    }
}