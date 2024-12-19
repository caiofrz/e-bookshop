using System.ComponentModel.DataAnnotations;

namespace backend_api.Domain.Models.Queries;

public class QueryParams
{

    [Range(1, int.MaxValue, ErrorMessage = "O número da página deve ser igual ou superior a 1")]
    public int Page { get; set; } = 1;
 
    [Range(1, 20, ErrorMessage = "A quantidade de registros por página deve ser entre {1} e {2}")]
    public int PageSize { get; set; } = 100;
}
