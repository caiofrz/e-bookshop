using System.Text.Json.Serialization;

namespace frontend.Domain.Models;

public class Sale
{
    [JsonPropertyName("items")]
    public List<SaleItem> Items { get; set; }

    [JsonPropertyName("total")]
    public decimal Total { get; set; }
}

public class SaleItem
{
    [JsonPropertyName("bookId")]
    public int BookId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}