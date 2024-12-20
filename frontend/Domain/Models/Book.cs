using System.Text.Json.Serialization;

namespace frontend.Domain.Models;

public class Book
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("isbn")]
    public string Isbn { get; set; }
    
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("authors")]
    public List<string> Authors { get; set; } = new();
    
    [JsonPropertyName("category")]
    public string Category { get; set; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    
    [JsonPropertyName("stockQuantity")]
    public int StockQuantity { get; set; }
}