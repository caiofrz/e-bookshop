using System.Text.Json;
using frontend.Application.Extensions;
using frontend.Domain.Enums;
using frontend.Domain.Models;
using Microsoft.AspNetCore.Mvc;
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
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalPages { get; set; }

    public async Task OnGetAsync(DateTime? startDate, DateTime? endDate, int pageNumber = 1)
    {
        Page = pageNumber;
        StartDate = startDate;
        EndDate = endDate;
        var queryParams = new List<string>();

        if (startDate.HasValue)
            queryParams.Add($"startDate={startDate.Value:yyyy-MM-ddTHH:mm:ss.fffZ}");

        if (endDate.HasValue)
            queryParams.Add($"endDate={endDate.Value:yyyy-MM-ddTHH:mm:ss.fffZ}");

        var queryString = string.Join("&", queryParams);
        var baseUri = $"{ApiEndpointEnum.Sales.Description()}?page={Page}&pageSize={PageSize}";
        var uri = string.IsNullOrEmpty(queryString) ? baseUri : $"{baseUri}&{queryString}";

        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ApiResponseSalesList>(json);

            if (apiResponse?.Registro?.Sales != null)
            {
                Sales = apiResponse.Registro.Sales;
                TotalPages = (int)Math.Ceiling((double)apiResponse.Registro.Pagination.TotalItems / PageSize);
            }
        }
    }
}

