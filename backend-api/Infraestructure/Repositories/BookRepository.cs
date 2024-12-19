using backend_api.Application.Utils;
using backend_api.Domain.Interfaces.Repositories;
using backend_api.Domain.Models;
using backend_api.Domain.Models.Queries;
using backend_api.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Infraestructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Book> _dbSet;

    public BookRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Book>();
    }

    public async Task<IEnumerable<Book>> GetAllAsync(BooksQueryParams queryParams)
    {
        var query = _context.Books.AsQueryable();

        if (queryParams.MaxStockLimit > 0)
            query = query.Where(book => book.StockQuantity <= queryParams.MaxStockLimit);

        return await query.Paginar(queryParams.Page, queryParams.PageSize)
                          .AsNoTracking()
                          .ToListAsync();
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await _context.Books
                             .AsNoTracking()
                             .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book> CreateAsync(Book book)
    {
        await _dbSet.AddAsync(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        _dbSet.Attach(book);
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByISBNAsync(string isbn)
    {
        return await _context.Books.AnyAsync(b => b.ISBN == isbn);
    }
}