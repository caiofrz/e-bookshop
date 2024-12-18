using backend_api.Domain.Models;

namespace backend_api.Domain.Interfaces.Repositories;

public interface ISaleRepository
{
    Task<Sale> RegisterSaleAsync(Sale sale);
}
