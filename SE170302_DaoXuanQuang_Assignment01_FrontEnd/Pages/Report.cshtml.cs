using Microsoft.AspNetCore.Mvc.RazorPages;
using SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs;
using System.Net.Http.Json;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.Pages
{
    public class ReportModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ReportModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public List<NewsArticleDto> ReportData { get; set; } = new();
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public async Task OnGetAsync(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;

            if (startDate.HasValue && endDate.HasValue)
            {
                var client = _httpClientFactory.CreateClient("ODataApi");
                var url = $"odata/NewsArticles?$filter=CreatedDate ge {startDate:yyyy-MM-dd} and CreatedDate le {endDate:yyyy-MM-dd}&$orderby=CreatedDate desc&$expand=Category";
                var response = await client.GetFromJsonAsync<ODataResponse<NewsArticleDto>>(url);
                if (response?.Value != null) ReportData = response.Value;
            }
        }
    }
}