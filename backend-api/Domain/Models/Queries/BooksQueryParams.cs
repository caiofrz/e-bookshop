namespace backend_api.Domain.Models.Queries;

public class BooksQueryParams : QueryParams
{
    public int? MaxStockLimit { get; set; }
}