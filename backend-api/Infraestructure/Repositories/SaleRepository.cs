using backend_api.Domain.Interfaces.Repositories;
using backend_api.Domain.Models;
using backend_api.Infraestructure.Data;

namespace backend_api.Infraestructure.Repositories;

public class SaleRepository: ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Sale> RegisterSaleAsync(Sale sale)
    {
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();
        return sale;
    }
}