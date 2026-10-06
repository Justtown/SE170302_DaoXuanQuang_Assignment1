using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs;
using System.Net.Http.Json;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.Pages.Accounts
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public SystemAccountDto Account { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(short id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var response = await client.GetFromJsonAsync<SystemAccountDto>($"odata/SystemAccounts({id})");
                if (response != null) Account = response;
                else return NotFound();
            }
            catch (Exception ex) { ErrorMessage = $"Load failed: {ex.Message}"; }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var response = await client.PutAsJsonAsync($"odata/SystemAccounts({Account.AccountId})", Account);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Account updated successfully.";
                    return RedirectToPage("./Index");
                }
                ErrorMessage = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex) { ErrorMessage = $"Update failed: {ex.Message}"; }
            return Page();
        }
    }
}