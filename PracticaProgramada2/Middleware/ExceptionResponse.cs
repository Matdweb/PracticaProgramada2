using System.Net;

namespace PracticaProgramada2.Middleware
{
    public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
}
