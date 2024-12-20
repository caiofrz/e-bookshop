namespace frontend.Domain.Models;

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