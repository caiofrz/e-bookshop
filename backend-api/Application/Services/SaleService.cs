using backend_api.Application.Exceptions;
using backend_api.Domain.Interfaces.Repositories;
using backend_api.Domain.Interfaces.Services;
using backend_api.Domain.Models;
using backend_api.Infraestructure.Data;

namespace backend_api.Application.Services;

public class SaleService : ISaleService
{
    private readonly IBookRepository _bookRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly AppDbContext _context;

    public SaleService(IBookRepository bookRepository,
                      ISaleRepository saleRepository,
                      AppDbContext context)
    {
        _bookRepository = bookRepository;
        _saleRepository = saleRepository;
        _context = context;
    }

    public async Task<Sale> RegisterSaleAsync(Sale sale)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            sale.SaleDate = DateTime.UtcNow;
            await UpdateBooksStock(sale);
            var registeredSale = await _saleRepository.RegisterSaleAsync(sale);

            await transaction.CommitAsync();

            return registeredSale;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task UpdateBooksStock(Sale sale)
    {
        foreach (var item in sale.Items)
        {
            var book = await _bookRepository.GetByIdAsync(item.BookId)
                       ?? throw new NotFoundException($"Livro com ID {item.BookId} não encontrado.");

            if (book.StockQuantity < item.Quantity)
                throw new UnprocessableEntityException($"Estoque insuficiente para o livro: '{book.Title}'.");

            book.StockQuantity -= item.Quantity;

            await _bookRepository.UpdateAsync(book);
        }
    }
}
