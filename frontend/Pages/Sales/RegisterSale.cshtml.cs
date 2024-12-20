using System.Text.Json;
using frontend.Application.Extensions;
using frontend.Domain.Enums;
using frontend.Domain.Models;
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
    public decimal Total { get; set; }

    public async Task OnGetAsync()
    {
        var uri = ApiEndpointEnum.Books.Description();
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponseBooksList>(json);

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

        Total = Books.Sum(item => item.Quantity * item.Price);

        var sale = new Sale
        {
            Items = Books.Select(item => new SaleItem
            {
                BookId = item.BookId,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList(),
            Total = Total
        };

        var uri = ApiEndpointEnum.Sales.Description();
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.PostAsJsonAsync(uri, sale);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("/Index");
        }

        ModelState.AddModelError(string.Empty, "Erro ao registrar venda.");
        return Page();
    }
}

