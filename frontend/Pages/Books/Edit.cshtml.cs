using frontend.Application.Extensions;
using frontend.Domain.Enums;
using frontend.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace frontend.Pages.Books;

public class EditModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    public Book Book { get; set; }
    private readonly string baseUri;

    public EditModel(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        baseUri = ApiEndpointEnum.Books.Description();
    }

    public async Task OnGetAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("API");
        var response = await client.GetAsync($"{baseUri}/{id}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ApiResponseBook>(json);

            if (apiResponse?.Registro != null)
            {
                Book = apiResponse?.Registro;
            }
        }
    }

    public async Task<IActionResult> OnPostAsync(int id, string title, string category, string Isbn, decimal price, int stockQuantity, string authors)
    {
        Book = new()
        {
            Title = title,
            Isbn = Isbn,
            Category = category,
            Price = price,
            StockQuantity = stockQuantity,
            Authors = JsonSerializer.Deserialize<List<string>>(authors)
        };

        var client = _httpClientFactory.CreateClient("API");
        var response = await client.PutAsJsonAsync($"{baseUri}/{id}", Book);

        if (!response.IsSuccessStatusCode)
        {
            var apiResponse = JsonSerializer.Deserialize<ApiErrorResponse>(await response.Content.ReadAsStringAsync());
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
                var errorDetails = JsonSerializer.Deserialize<List<ErrorDetail>>(jsonElement.GetRawText());
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
