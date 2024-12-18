namespace backend_api.UI.Responses;

public class ApiErrorResponse
{
    public string Titulo { get; set; }
    public int Status { get; set; }
    public object Detalhe { get; set; }
}
