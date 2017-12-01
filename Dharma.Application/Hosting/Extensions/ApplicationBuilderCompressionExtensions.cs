using Microsoft.AspNetCore.Builder;

namespace Dharma.Application.Hosting.Extensions
{
    /// <summary>
    /// Extensões para as configurações da compressão.
    /// </summary>
    public static class ApplicationBuilderCompressionExtensions
    {
        /// <summary>
        /// Habilita um middleware para segurança.
        /// </summary>
        /// <param name="app">Construtor da aplicação.</param>
        /// <returns>Construtor da aplicação.</returns>
        public static IApplicationBuilder UseCompression(this IApplicationBuilder app)
        {
            app.UseResponseCompression();

            return app;
        }
    }
}
