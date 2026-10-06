using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs;
using System.Net.Http.Json;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public CategoryDto Category { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(short id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var response = await client.GetFromJsonAsync<CategoryDto>($"odata/Categories({id})");
                if (response != null) Category = response;
                else return NotFound();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Load failed: {ex.Message}";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var response = await client.PutAsJsonAsync($"odata/Categories({Category.CategoryId})", Category);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Category updated successfully.";
                    return RedirectToPage("./Index");
                }
                ErrorMessage = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Update failed: {ex.Message}";
            }
            return Page();
        }
    }
}