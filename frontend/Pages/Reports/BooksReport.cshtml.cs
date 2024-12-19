
using System.Text.Json;
using frontend.Pages.Books;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend.Pages.Reports;

public class BooksReportModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BooksReportModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IEnumerable<Book> Books { get; set; }
    public int? MaxStockLimit { get; set; }

    public async Task OnGetAsync(int? maxStockLimit)
    {
        MaxStockLimit = maxStockLimit;

        var client = _httpClientFactory.CreateClient("API");
        var uri = "api/books";
        uri += maxStockLimit > 0 ? $"?maxStockLimit={maxStockLimit}" : "";

        var response = await client.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ApiResponseListagem>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (apiResponse?.Registro?.Books != null)
            {
                Books = apiResponse.Registro.Books;
            }
        }
    }
}