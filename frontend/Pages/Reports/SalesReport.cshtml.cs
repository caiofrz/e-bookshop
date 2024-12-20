using System.Text.Json;
using frontend.Application.Extensions;
using frontend.Domain.Enums;
using frontend.Domain.Models;
using frontend.Pages.Books;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend.Pages.Reports;

public class SalesReportModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public SalesReportModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IEnumerable<SaleResponse> Sales { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public async Task OnGetAsync(DateTime? startDate, DateTime? endDate)
    {
        var queryParams = new List<string>();
        StartDate = startDate;
        EndDate = endDate;

        if (startDate.HasValue)
            queryParams.Add($"startDate={startDate.Value:yyyy-MM-ddTHH:mm:ss.fffZ}");

        if (endDate.HasValue)
            queryParams.Add($"endDate={endDate.Value:yyyy-MM-ddTHH:mm:ss.fffZ}");

        var queryString = string.Join("&", queryParams);
        var baseUri = ApiEndpointEnum.Sales.Description();
        var uri = string.IsNullOrEmpty(queryString) ? baseUri : $"{baseUri}?{queryString}";

        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ApiResponseSalesListagem>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (apiResponse?.Registro?.Sales != null)
            {
                Sales = apiResponse.Registro.Sales;
            }
        }
    }
}

public class Registro
{
    public List<SaleResponse> Sales { get; set; }
    public Pagination Pagination { get; set; }
}

public class ApiResponseSalesListagem : ApiResponse
{
    public Registro Registro { get; set; }
}

public class SaleResponse
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; }
    public List<SaleItemResponse> Items { get; set; }
    public decimal Total { get; set; }
}

public class SaleItemResponse
{
    public int Id { get; set; }
    public Book Book { get; set; }
    public int Quantity { get; set; }
}
