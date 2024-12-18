namespace backend_api.Application.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string ISBN { get; set; }
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}