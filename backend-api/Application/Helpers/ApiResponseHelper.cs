using backend_api.UI.Responses;

namespace backend_api.Application.Helpers;

public class ApiResponseHelper
{
    public static ApiResponse<T> CriarRespostaSucesso<T>(int status,
                                                        string detalhe,
                                                        T registro,
                                                        string titulo = "Sucesso")
    {
        return new ApiResponse<T>
        {
            Titulo = titulo,
            Status = status,
            Detalhe = detalhe,
            Registro = registro
        };
    }

    public static ApiErrorResponse CriarRespostaErro(int status,
                                                    object detalhe,
                                                    string titulo = "Erro")
    {
        return new ApiErrorResponse
        {
            Titulo = titulo,
            Status = status,
            Detalhe = detalhe,
        };
    }
}
