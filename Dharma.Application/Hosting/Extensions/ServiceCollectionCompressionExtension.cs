using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace Dharma.Application.Hosting.Extensions
{
    // <summary>
    /// Extensões para configurações da injeção de dependência para a compressão de resposta.
    /// </summary>
    public static class ServiceCollectionCompressionExtension
    {
        /// <summary>
        /// Configura os valores padrões para as chaves de configuração do sistema.
        /// </summary>
        /// <param name="services">Configurador de serviços da aplicação web.</param>
        /// <returns>Configurador de serviços da aplicação web.</returns>
        public static IServiceCollection AddCompression(this IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = new[]
                {
                    // Default
                    "text/plain",
                    "text/css",
                    "application/javascript",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                    // Custom
                    "application/font-woff",
                    "image/png"
                };
            });

            return services;
        }
    }
}
