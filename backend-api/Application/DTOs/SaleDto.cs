using backend_api.Domain.Models;

namespace backend_api.Application.DTOs;

public class SaleDto
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; }
    public ICollection<SaleItem> Items { get; set; }
}