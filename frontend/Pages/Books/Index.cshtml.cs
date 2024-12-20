using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace frontend.Pages.Books;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public List<Book> Books { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalPages { get; set; }

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        Books = new();
    }

    public async Task OnGetAsync(int pageNumber = 1)
    {
        Page = pageNumber;
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetAsync($"api/books?page={Page}&pageSize={PageSize}");

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
                TotalPages = (int)Math.Ceiling((double)apiResponse.Registro.Pagination.TotalItems / PageSize);
            }
        }
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.DeleteAsync($"api/books/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }

        return Page();
    }
}


public class Book
{
    public int Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public List<string> Authors { get; set; } = new();
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

public class Pagination
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
}


public class ApiResponse
{
    public string Titulo { get; set; }
    public int Status { get; set; }
    public string Detalhe { get; set; }
}

public class Registro
{
    public List<Book> Books { get; set; }
    public Pagination Pagination { get; set; }
}

public class ApiResponseListagem : ApiResponse
{
    public Registro Registro { get; set; }
}