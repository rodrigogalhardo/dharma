using Google.Cloud.Diagnostics.AspNetCore;
using Microsoft.AspNetCore.Builder;

namespace Dharma.Application.Hosting.Extensions
{
    /// <summary>
    /// Extensões para as configurações de log do hosting.
    /// </summary>
    public static class ApplicationBuilderLogExtensions
    {
        /// <summary>
        /// Habilita um middleware para segurança.
        /// </summary>
        /// <param name="app">Construtor da aplicação.</param>
        /// <returns>Construtor da aplicação.</returns>
        public static IApplicationBuilder UseLog(this IApplicationBuilder app)
        {
            app.UseGoogleExceptionLogging();
            app.UseGoogleTrace();

            return app;
        }
    }
}
