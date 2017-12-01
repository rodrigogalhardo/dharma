using Microsoft.AspNetCore.Builder;

namespace Dharma.Application.Hosting.Extensions
{
    /// <summary>
    /// Extensões para as configurações de segurança no hosting.
    /// </summary>
    public static class ApplicationBuilderSecurityExtensions
    {
        /// <summary>
        /// Habilita um middleware para segurança.
        /// </summary>
        /// <param name="app">Construtor da aplicação.</param>
        /// <param name="startup">Inicializador da hospedagem.</param>
        /// <returns>Construtor da aplicação.</returns>
        public static IApplicationBuilder UseSecurity(this IApplicationBuilder app, BaseStartup startup)
        {
            app.UseAuthentication();

            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
                options.AllowCredentials();
            });

            return app;
        }
    }
}
