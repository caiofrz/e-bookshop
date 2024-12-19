using System.Text.Json;
using frontend.Pages.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class RegisterSaleModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RegisterSaleModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public List<SaleItem> Books { get; set; } = new();
    public List<Book> AvailableBooks { get; set; } = new();

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
                AvailableBooks = apiResponse.Registro.Books;
            }
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var sale = new Sale
        {
            Items = Books.Select(item => new SaleItem
            {
                BookId = item.BookId,
                Quantity = item.Quantity
            }).ToList()
        };

        var client = _httpClientFactory.CreateClient("API");
        var response = await client.PostAsJsonAsync("api/sales", sale);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("/Index");
        }

        ModelState.AddModelError(string.Empty, "Erro ao registrar venda.");
        return Page();
    }
}

public class Sale
{
    public List<SaleItem> Items { get; set; }
}

public class SaleItem
{
    public int BookId { get; set; }
    public int Quantity { get; set; }
}
