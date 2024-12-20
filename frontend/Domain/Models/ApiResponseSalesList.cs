using System.Text.Json.Serialization;

namespace frontend.Domain.Models;

public class ApiResponseSalesList
{
    [JsonPropertyName("registro")]
    public RegistroSales Registro { get; set; }
}

public class RegistroSales
{
    [JsonPropertyName("sales")]
    public List<SaleResponse> Sales { get; set; }

    [JsonPropertyName("pagination")]
    public Pagination Pagination { get; set; }
}

public class SaleResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("saleDate")]
    public DateTime SaleDate { get; set; }

    [JsonPropertyName("items")]
    public List<SaleItemResponse> Items { get; set; }

    [JsonPropertyName("total")]
    public decimal Total { get; set; }
}

public class SaleItemResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("book")]
    public Book Book { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}
