using backend_api.Domain.Models;
using backend_api.Domain.Models.Queries;

namespace backend_api.Domain.Interfaces.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync(BooksQueryParams queryParams);
    Task<Book> GetByIdAsync(int id);
    Task<Book> CreateAsync(Book entity);
    Task<Book> UpdateAsync(Book entity);
    Task DeleteAsync(int id);
    Task<bool> ExistsByISBNAsync(string isbn);
}
