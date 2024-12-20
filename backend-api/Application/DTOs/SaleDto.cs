using backend_api.Domain.Models;

namespace backend_api.Application.DTOs;

public class SaleResponseDto
{
    public IEnumerable<SaleDto> Sales { get; set; }
    public PaginationResponseDto Pagination { get; set; } = new();

    public SaleResponseDto(IEnumerable<SaleDto> sales, int page, int pageSize, int totalItems)
    {
        Sales = sales;
        Pagination = new()
        {
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }
}

public class SaleDto
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; }
    public ICollection<SaleItemDto> Items { get; set; }
    public decimal Total { get; set; }
}

public class SaleItemDto
{
    public int Id { get; set; }
    public Book Book { get; set; }
    public int Quantity { get; set; }
}
