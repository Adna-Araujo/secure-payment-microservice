using System.Net;
using SecurePaymentService.Models;

namespace SecurePaymentService.Middleware;

public class ExceptionMiddleware {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger) {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext) {
        try {
            await _next(httpContext);
        } catch (Exception ex) {
           _logger.LogError("Erro processado pelo Middleware: {Message} | Local: {StackTrace}", 
                ex.Message, 
                ex.StackTrace);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception) {
        context.Response.ContentType = "application/json";
        
        // Se for um erro de argumento (como o nosso do cartão), mandamos 400. 
        // Senão, mandamos 500 (Erro interno).
        context.Response.StatusCode = exception is ArgumentException 
            ? (int)HttpStatusCode.BadRequest 
            : (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(new ErrorDetails() {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message
        }.ToString());
    }
}