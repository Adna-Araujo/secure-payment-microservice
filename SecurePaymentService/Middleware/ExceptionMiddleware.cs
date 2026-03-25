using System.Net;
using SecurePaymentService.Models;

namespace SecurePaymentService.Middleware;

public class ExceptionMiddleware {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env) {
        _next = next;
        _logger = logger;
        _env = env;
    }
    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context); // Tenta seguir com a requisição normal
        }
        catch (Exception ex) {
            _logger.LogError(ex, ex.Message); // Registra o erro no log/terminal
            await HandleExceptionAsync(context, ex); // Trata o erro de forma amigável
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception) {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new ErrorDetails {
            StatusCode = context.Response.StatusCode,
            Message = "Ocorreu um erro interno no servidor de pagamentos.",
            // Só mostra o erro detalhado se estivermos em ambiente de desenvolvimento
            Trace = _env.IsDevelopment() ? exception.StackTrace?.ToString() : null
        };
        await context.Response.WriteAsync(response.ToString());
    }
}