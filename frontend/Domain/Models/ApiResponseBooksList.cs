using System.Text.Json.Serialization;

namespace frontend.Domain.Models;

public class ApiResponseBooksList : ApiResponse
{
    [JsonPropertyName("registro")]
    public RegistroBooks Registro { get; set; }
}

public class RegistroBooks
{
    [JsonPropertyName("books")]
    public List<Book> Books { get; set; }

    [JsonPropertyName("pagination")]
    public Pagination Pagination { get; set; }
}