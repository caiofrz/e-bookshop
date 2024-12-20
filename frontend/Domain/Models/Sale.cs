namespace frontend.Domain.Models;

public class Sale
{
    public List<SaleItem> Items { get; set; }
    public decimal Total { get; set; }
}

public class SaleItem
{
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}