using System.ComponentModel;

namespace frontend.Domain.Enums;

public enum ApiEndpointEnum
{
    [Description("api/books")]
    Books,
    [Description("api/sales")]
    Sales,
}