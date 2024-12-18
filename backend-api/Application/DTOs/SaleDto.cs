using backend_api.Domain.Models;

namespace backend_api.Application.DTOs;

public class SaleDto
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; }
    public ICollection<SaleItemDto> Items { get; set; }
}

public class SaleItemDto
{
    public int Id { get; set; }
    public Book Book { get; set; }
    public int Quantity { get; set; }
}
