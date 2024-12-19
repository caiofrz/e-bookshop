namespace backend_api.Domain.Models.Queries;

public class SalesQueryParams : QueryParams
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; } = DateTime.UtcNow;
}
