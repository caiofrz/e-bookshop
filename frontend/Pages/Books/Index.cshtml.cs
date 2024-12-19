using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace frontend.Pages.Books;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public List<Book> Books { get; set; } = new();

    public IndexModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task OnGetAsync()
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetAsync("api/books");

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
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.DeleteAsync($"api/books/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage();
        }

        ModelState.AddModelError("", "");
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