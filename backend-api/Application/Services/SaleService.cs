using backend_api.Application.Exceptions;
using backend_api.Domain.Interfaces.Repositories;
using backend_api.Domain.Interfaces.Services;
using backend_api.Domain.Models;

namespace backend_api.Application.Services;

public class SaleService: ISaleService
{
    private readonly IBookRepository _bookRepository;
    private readonly ISaleRepository _saleRepository;

    public SaleService(IBookRepository bookRepository, ISaleRepository saleRepository)
    {
        _bookRepository = bookRepository;
        _saleRepository = saleRepository;
    }

    public async Task<Sale> RegisterSaleAsync(Sale sale)
    {
        sale.SaleDate = DateTime.Now;

        await UpdateBooksStock(sale);

        return await _saleRepository.RegisterSaleAsync(sale);
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

            sale.Items.Add(new SaleItem
            {
                BookId = book.Id,
                Quantity = item.Quantity
            });

            await _bookRepository.UpdateAsync(book);
        }
    }
}
