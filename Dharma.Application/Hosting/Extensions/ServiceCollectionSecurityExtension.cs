using Dharma.Application.Configurations;
using Dharma.Application.Hosting.Enums;
using Dharma.Common.Helpers;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Dharma.Application.Hosting.Extensions
{
    /// <summary>
    /// Extensões para as configurações de segurança nos serviços.
    /// </summary>
    public static class ServiceCollectionSecurityExtension
    {
        /// <summary>
        /// Configura os valores padrões para as chaves de configuração do sistema.
        /// </summary>
        /// <param name="services">Configurador de serviços da aplicação web.</param>
        /// <param name="startup">Inicializador da hospedagem.</param>
        /// <returns>Configurador de serviços da aplicação web.</returns>
        public static IServiceCollection AddSecurity(this IServiceCollection services, BaseStartup startup)
        {
            services.AddCors();
            
            switch (startup.Startup)
            {
                case StartupEnum.UI:
                    services.AddSecurityForUI(startup);
                    break;

                case StartupEnum.API:
                    services.AddSecurityForAPI(startup);
                    break;

                default:
                    throw new NotImplementedException("Não implementado camada de segurança para o tipo: " + Enum.GetName(typeof(StartupEnum), startup.Startup) + ".");
            }

            return services;
        }

        private static IServiceCollection AddSecurityForUI(this IServiceCollection services, BaseStartup startup)
        {
            var config = OptionsHelper.GetConfiguration<SecurityUIOptions>(startup.Configuration);

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";

                    options.Authority = config.Authority;
                    options.RequireHttpsMetadata = false;

                    options.ClientId = config.ClientId;
                    options.ClientSecret = config.ClientSecret;
                    options.ResponseType = "code id_token";

                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    foreach (var item in config.Scopes)
                    {
                        options.Scope.Add(item);
                    }
                });

            return services;
        }

        private static IServiceCollection AddSecurityForAPI(this IServiceCollection services, BaseStartup startup)
        {
            var config = OptionsHelper.GetConfiguration<SecurityApiOptions>(startup.Configuration);

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = config.Authority;
                        options.RequireHttpsMetadata = false;
                        options.ApiName = config.ApiName;
                        options.ApiSecret = config.ApiSecret;
                    });

            return services;
        }
    }
}
