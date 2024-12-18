namespace backend_api.UI.Responses;

public class ApiResponse<T>
{
    public string Titulo { get; set; }
    public int Status { get; set; }
    public string Detalhe { get; set; }
    public T Registro { get; set; }
}