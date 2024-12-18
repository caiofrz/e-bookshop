using backend_api.Domain.Models;

namespace backend_api.Domain.Interfaces.Services;

public interface ISaleService
{
    Task<Sale> RegisterSaleAsync(Sale sale);
}
