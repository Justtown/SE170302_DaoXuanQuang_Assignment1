using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs;
using System.Net.Http.Json;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<SystemAccountDto> Accounts { get; set; } = new();
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var response = await client.GetFromJsonAsync<ODataResponse<SystemAccountDto>>("odata/SystemAccounts");
                if (response?.Value != null) Accounts = response.Value;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Unable to load accounts: {ex.Message}";
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(short id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var response = await client.DeleteAsync($"odata/SystemAccounts({id})");
                if (response.IsSuccessStatusCode)
                    TempData["Success"] = "Account deleted successfully.";
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