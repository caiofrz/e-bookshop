namespace frontend.Domain.Models;

public class Pagination
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
}