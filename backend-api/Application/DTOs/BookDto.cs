namespace backend_api.Application.DTOs;

public class BooksResponseDto
{
    public IEnumerable<BookDto> Books { get; set; }
    public PaginationResponseDto Pagination { get; set; } = new();

    public BooksResponseDto(IEnumerable<BookDto> books, params int[] pagination)
    {
        Books = books;
        Pagination = new()
        {
            Page = pagination[0],
            PageSize = pagination[1],
        };
    }

}

public class BookDto
{
    public int Id { get; set; }
    public string ISBN { get; set; }
    public string Title { get; set; }
    public List<string> Authors { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}