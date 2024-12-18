using System.Net;
using System.Text.Json;
using backend_api.Application.Exceptions;
using backend_api.Application.Helpers;

namespace backend_api.Application.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
        }
        catch (BadHttpRequestException ex)
        {
            _logger.LogError(ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (ConflictException ex)
        {
            _logger.LogError(ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict);
        }
        catch (UnprocessableEntityException ex)
        {
            _logger.LogError(ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.UnprocessableEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, true);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode, bool exceptionGenerica = false)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)httpStatusCode;

        string errorMessage;

        if (exceptionGenerica)
            errorMessage = "Ocorreu um erro interno no servidor.";
        else
            errorMessage = exception.Message;

        var response = ApiResponseHelper.CriarRespostaErro(context.Response.StatusCode, errorMessage);

        var jsonResponse = JsonSerializer.Serialize(response);

        return context.Response.WriteAsync(jsonResponse);
    }
}