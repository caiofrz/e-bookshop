using frontend.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace frontend.Pages.Books;

public class CreateModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    public Book Book { get; set; }

    public CreateModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        Book = new();
    }

    public async Task<IActionResult> OnPostAsync(string title, string category, string Isbn, decimal price, int stockQuantity, string authors)
    {
        Book = new Book
        {
            Title = title,
            Isbn = Isbn,
            Category = category,
            Price = price,
            StockQuantity = stockQuantity,
            Authors = JsonSerializer.Deserialize<List<string>>(authors)
        };

        var client = _httpClientFactory.CreateClient("API");
        var response = await client.PostAsJsonAsync("api/books", Book);

        if (!response.IsSuccessStatusCode)
        {
            var apiResponse = JsonSerializer.Deserialize<ApiErrorResponse>(await response.Content.ReadAsStringAsync(),
                                                                           new JsonSerializerOptions
                                                                           {
                                                                               PropertyNameCaseInsensitive = true
                                                                           });
            foreach (var error in apiResponse?.Detalhe)
            {
                var key = error.Key;
                var messages = error.Value;

                foreach (var message in messages)
                {
                    ModelState.AddModelError(key, message);
                }
            }
            return Page();
        }
        return RedirectToPage("/Books/Index");
    }
}

