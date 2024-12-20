using System.Text.Json.Serialization;

namespace frontend.Domain.Models;

public class ApiResponseBook : ApiResponse
{
    [JsonPropertyName("registro")]
    public Book Registro { get; set; }
}