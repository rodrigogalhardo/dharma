using Microsoft.Extensions.Configuration;
using System;

namespace Dharma.Common.Tests
{
    /// <summary>
    /// Base para os testes unitários.
    /// </summary>
    public abstract class BaseTest
    {
        /// <summary>
        /// Dados do arquivo de configuração do projeto (appsettings.json).
        /// </summary>
        protected static IConfigurationRoot Configuration { get; private set; }

        static BaseTest()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        /// <summary>
        /// Gera um nome randômico com prefixo.
        /// </summary>
        /// <param name="prefix">Prefixo.</param>
        /// <returns>Nome randômico com prefixo.</returns>
        protected static string GetUniqueName(string prefix)
        {
            return prefix + "_" + GetUniqueName(12);
        }

        /// <summary>
        /// Gera uma string randômica com o número de caracteres solicitados.
        /// </summary>
        /// <param name="strLen">Tamnho do texto que será gerado.</param>
        /// <returns>String randômica.</returns>
        protected static string GetUniqueName(int strLen)
        {
            string randomStr = string.Empty;

            while (randomStr.Length < strLen)
            {
                randomStr += Guid.NewGuid().ToString().Replace("-", "");

                if (randomStr.Length > strLen)
                {
                    randomStr = randomStr.Substring(0, strLen);
                }
            }

            return randomStr;
        }
    }
}
