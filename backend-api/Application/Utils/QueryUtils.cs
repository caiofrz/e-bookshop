namespace backend_api.Application.Utils;

public static class QueryUtils
{
    public static IQueryable<T> Paginar<T>(this IQueryable<T> query, int page, int pageSize) => query.Skip((page - 1) * pageSize).Take(pageSize);
}