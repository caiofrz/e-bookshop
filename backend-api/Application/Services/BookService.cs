using backend_api.Application.Exceptions;
using backend_api.Domain.Interfaces.Repositories;
using backend_api.Domain.Interfaces.Services;
using backend_api.Domain.Models;

namespace backend_api.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await _bookRepository.GetByIdAsync(id)
               ?? throw new NotFoundException("Livro não encontrado");
    }

    public async Task<Book> CreateAsync(Book book)
    {
        if (await _bookRepository.ExistsByISBNAsync(book.ISBN))
            throw new ConflictException("ISBN já cadastrado para outro livro");

        return await _bookRepository.CreateAsync(book);
    }

    public async Task<Book> UpdateAsync(int id, Book updatedBook)
    {
        var book = await _bookRepository.GetByIdAsync(id)
                    ?? throw new NotFoundException("Livro não encontrado");

        if (book.ISBN != updatedBook.ISBN)
        {
            if (await _bookRepository.ExistsByISBNAsync(updatedBook.ISBN))
                throw new ConflictException("ISBN já cadastrado para outro livro");
        }
        updatedBook.Id = book.Id;

        return await _bookRepository.UpdateAsync(updatedBook);
    }

    public async Task DeleteAsync(int id)
    {
        await _bookRepository.DeleteAsync(id);
    }
}