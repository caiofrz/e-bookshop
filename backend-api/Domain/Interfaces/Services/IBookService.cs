using backend_api.Domain.Models;
using backend_api.Domain.Models.Queries;

namespace backend_api.Domain.Interfaces.Services;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllAsync(BooksQueryParams queryParams);
    Task<Book> GetByIdAsync(int id);
    Task<Book> CreateAsync(Book book);
    Task<Book> UpdateAsync(int id, Book updatedBook);
    Task DeleteAsync(int id);
}