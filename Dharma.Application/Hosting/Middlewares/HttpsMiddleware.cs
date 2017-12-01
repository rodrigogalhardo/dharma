using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Net;
using System.Threading.Tasks;

namespace Dharma.Application.Hosting.Middlewares
{
    /// <summary>
    /// Middleware para impedir o uso de HTTP em produção.
    /// </summary>
    public class HttpsMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Construtor do middleware.
        /// </summary>
        /// <param name="next">Request Delegate.</param>
        public HttpsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Execução da captura.
        /// </summary>
        /// <param name="context">Contexto HTTP.</param>
        /// <returns>Tarefa.</returns>
        public async Task Invoke(HttpContext context)
        {
            string protocol = context.Request.Headers["x-forwarded-proto"];

            if (protocol == "http")
            {
                string newUrl = context.Request.GetDisplayUrl();

                newUrl = newUrl.Replace("http://", "https://");

                context.Response.StatusCode = (int)HttpStatusCode.MovedPermanently;
                context.Response.Headers.Add("Location", newUrl);

                await context.Response.WriteAsync(string.Empty).ConfigureAwait(false);
            }
            else
            {
                await _next(context);
            }
        }
    }
}
