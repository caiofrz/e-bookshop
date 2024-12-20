using System.Text.Json.Serialization;

namespace frontend.Domain.Models;

public class ApiErrorResponse
{
    [JsonPropertyName("titulo")]
    public string Titulo { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("detalhe")]
    public object Detalhe { get; set; }
}

public class ErrorDetail
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("value")]
    public List<string> Value { get; set; }
}
