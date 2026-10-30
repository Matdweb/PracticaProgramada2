using System.Net;

namespace PracticaProgramada2.Middleware
{
    public class MiddlewareGlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public MiddlewareGlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ExceptionResponse response = exception switch
            {
                KeyNotFoundException => new ExceptionResponse(HttpStatusCode.NotFound, exception.Message + "No se ha encontrado del recurso"),
                UnauthorizedAccessException => new ExceptionResponse(HttpStatusCode.Unauthorized, exception.Message + "No tiene permisos para acceder a este recurso"),
                NotImplementedException => new ExceptionResponse(HttpStatusCode.NotImplemented, exception.Message + "la funcionalidad no está implementada"),
                _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Ocurrió un error inesperado.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
