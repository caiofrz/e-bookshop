using backend_api.Domain.Models;
using backend_api.Domain.Models.Queries;

namespace backend_api.Domain.Interfaces.Services;

public interface ISaleService
{
    Task<IEnumerable<Sale>> GetAllAsync(SalesQueryParams queryParams);
    Task<Sale> RegisterSaleAsync(Sale sale);
    Task<int> GetTotalCountAsync();
}
