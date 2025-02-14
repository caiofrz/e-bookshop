using System.Text.Json;
using frontend.Application.Extensions;
using frontend.Domain.Enums;
using frontend.Domain.Models;
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
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalPages { get; set; }

    public async Task OnGetAsync(int? maxStockLimit, int pageNumber = 1)
    {
        Page = pageNumber;
        MaxStockLimit = maxStockLimit;

        var client = _httpClientFactory.CreateClient("API");
        var uri = $"{ApiEndpointEnum.Books.Description()}?page={Page}&pageSize={PageSize}";
        uri += maxStockLimit > 0 ? $"&maxStockLimit={maxStockLimit}" : "";

        var response = await client.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ApiResponseBooksList>(json);

            if (apiResponse?.Registro?.Books != null)
            {
                Books = apiResponse.Registro.Books;
                TotalPages = (int)Math.Ceiling((double)apiResponse.Registro.Pagination.TotalItems / PageSize);
            }
        }
    }
}