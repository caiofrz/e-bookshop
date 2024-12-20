namespace frontend.Domain.Models;

public class ApiErrorResponse
{
    public string Titulo { get; set; }
    public int Status { get; set; }
    public object Detalhe { get; set; }
}

public class ErrorDetail
{
    public string Key { get; set; }
    public List<string> Value { get; set; }
}
