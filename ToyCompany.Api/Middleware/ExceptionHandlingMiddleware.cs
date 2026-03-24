using System.Net;
using System.Text.Json;
using ToyCompany.Domain.Exceptions;

namespace ToyCompany.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
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
        catch (DomainValidationException exception)
        {
            await WriteProblemDetailsAsync(context, HttpStatusCode.BadRequest, "Erro de validacao", exception.Message);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Erro nao tratado ao processar a requisicao.");
            await WriteProblemDetailsAsync(context, HttpStatusCode.InternalServerError, "Erro interno", "Ocorreu um erro interno ao processar a requisicao.");
        }
    }

    private static async Task WriteProblemDetailsAsync(HttpContext context, HttpStatusCode statusCode, string title, string detail)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var payload = new
        {
            title,
            status = (int)statusCode,
            detail
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
}
