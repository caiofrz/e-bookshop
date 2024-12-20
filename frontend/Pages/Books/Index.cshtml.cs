using frontend.Application.Extensions;
using frontend.Domain.Enums;
using frontend.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace frontend.Pages.Books;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string baseUri;
    public List<Book> Books { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalPages { get; set; }

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        Books = new();
        baseUri = ApiEndpointEnum.Books.Description();
    }

    public async Task OnGetAsync(int pageNumber = 1)
    {
        Page = pageNumber;
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetAsync($"{baseUri}?page={Page}&pageSize={PageSize}");

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

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.DeleteAsync($"{baseUri}/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }

        return Page();
    }
}
