using System.Text.Json.Serialization;

namespace frontend.Domain.Models;

public class ApiResponse
{
    [JsonPropertyName("titulo")]
    public string Titulo { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("detalhe")]
    public object Detalhe { get; set; }
}