using backend_api.Application.Utils;
using backend_api.Domain.Interfaces.Repositories;
using backend_api.Domain.Models;
using backend_api.Domain.Models.Queries;
using backend_api.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Infraestructure.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sale>> GetAllAsync(SalesQueryParams queryParams)
    {
        var query = _context.Sales.AsQueryable();

        if (queryParams.StartDate is not null)
            query = query.Where(sale => sale.SaleDate >= queryParams.StartDate
                                        && sale.SaleDate <= queryParams.EndDate);

        return await query.OrderByDescending(sale => sale.SaleDate)
                          .Paginar(queryParams.Page, queryParams.PageSize)
                          .Include(sale => sale.Items)
                          .ThenInclude(item => item.Book)
                          .AsNoTracking()
                          .ToListAsync();
    }

    public async Task<Sale> RegisterSaleAsync(Sale sale)
    {
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await _context.Sales.CountAsync();
    }
}