using frontend.Application.Extensions;
using frontend.Domain.Enums;
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

        var uri = ApiEndpointEnum.Books.Description();
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.PostAsJsonAsync(uri, Book);

        if (!response.IsSuccessStatusCode)
        {
            var apiResponse = JsonSerializer.Deserialize<ApiErrorResponse>(await response.Content.ReadAsStringAsync(),
                                                                           new JsonSerializerOptions
                                                                           {
                                                                               PropertyNameCaseInsensitive = true
                                                                           });

            SetErrorDetails(apiResponse);
            return Page();
        }
        return RedirectToPage("/Books/Index");
    }

    private void SetErrorDetails(ApiErrorResponse apiResponse)
    {
        if (apiResponse?.Detalhe is JsonElement jsonElement)
        {
            if (jsonElement.ValueKind == JsonValueKind.String)
            {
                var errorMessage = jsonElement.GetString();
                ModelState.AddModelError("Error", errorMessage);
            }
            else if (jsonElement.ValueKind == JsonValueKind.Array)
            {
                var errorDetails = JsonSerializer.Deserialize<List<ErrorDetail>>(jsonElement.GetRawText(),
                                                                        new JsonSerializerOptions
                                                                        {
                                                                            PropertyNameCaseInsensitive = true
                                                                        });
                foreach (var error in errorDetails)
                {
                    var key = error.Key;
                    var messages = error.Value;

                    foreach (var message in messages)
                    {
                        ModelState.AddModelError(key, message);
                    }
                }
            }
        }
    }
}

