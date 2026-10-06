using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs;
using System.Net.Http.Json;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public LoginDto LoginData { get; set; } = new LoginDto();

        public string? ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                var client = _httpClientFactory.CreateClient("ODataApi");

                // Gọi OData filter để tìm account theo Email + Password
                var filterUrl = $"odata/SystemAccounts?$filter=AccountEmail eq '{LoginData.Email}' and AccountPassword eq '{LoginData.Password}'";
                var response = await client.GetFromJsonAsync<ODataResponse<SystemAccountDto>>(filterUrl);

                if (response != null && response.Value != null && response.Value.Any())
                {
                    var account = response.Value.First();

                    HttpContext.Session.SetString("AccountId", account.AccountId.ToString());
                    HttpContext.Session.SetString("AccountName", account.AccountName ?? "User");
                    HttpContext.Session.SetString("AccountRole", account.AccountRole?.ToString() ?? "3");

                    return account.AccountRole switch
                    {
                        1 => RedirectToPage("/News/Index"),   
                        2 => RedirectToPage("/News/Index"),   
                        _ => RedirectToPage("/Index")
                    };
                }

                ErrorMessage = "Invalid email or password.";
                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Login failed: {ex.Message}";
                return Page();
            }
        }
    }
}