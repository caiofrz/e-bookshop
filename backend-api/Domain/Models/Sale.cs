namespace backend_api.Domain.Models;

public class Sale
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; }
    public ICollection<SaleItem> Items { get; set; }
    public decimal Total { get; set; }
}
public class SaleItem
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int Quantity { get; set; }
}