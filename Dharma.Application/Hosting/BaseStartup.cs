using Dharma.Application.Hosting.Enums;
using Dharma.Application.Hosting.Extensions;
using Dharma.Application.Hosting.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dharma.Application.Hosting
{
    /// <summary>
    /// Configuração de startup do servidor de aplicação web.
    /// </summary>
    public abstract class BaseStartup
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="configuration">Configuração de inicilização.</param>
        /// <param name="logger">Configuração de log.</param>
        /// <param name="startup">Tipo de serviço web que deve ser inicializado.</param>
        protected BaseStartup(IConfiguration configuration, ILogger<BaseStartup> logger, StartupEnum startup)
        {
            Configuration = configuration;
            Logger = logger;
            Startup = startup;
        }

        /// <summary>
        /// Log da classe.
        /// </summary>
        public ILogger<BaseStartup> Logger { get; }

        /// <summary>
        /// Acesso as configurações.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Tipo de serviço web que deve ser inicializado.
        /// </summary>
        public StartupEnum Startup { get; }

        /// <summary>
        /// Configuração dos serviços da aplicação web.
        /// </summary>
        /// <param name="services">Configurador de serviços da aplicação web.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLog(Configuration);
            services.AddMvc();
            services.AddCompression();
            services.AddConfigurations(this);
            services.AddMappers(this);
            services.AddRepositories(this);
            services.AddBaseServices();
            services.AddSecurity(this);
        }

        /// <summary>
        /// Configuração do pipeline da aplicação web.
        /// </summary>
        /// <param name="app">Configurador da aplicação.</param>
        /// <param name="env">Configurador do ambiente.</param>
        /// <param name="loggerFactory">Fábrica de log.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            string pathBase = GetPathBase();

            if (!string.IsNullOrWhiteSpace(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            app.UseLog();
            app.UseMiddleware<HttpsMiddleware>();
            app.UseSecurity(this);
            app.UseCompression();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        /// <summary>
        /// Informa a URL base para as requisições para essa API. Exemplo: "/accounts/v1".
        /// </summary>
        /// <returns>URL base para as requisições dessa API.</returns>
        public abstract string GetPathBase();
    }

}
