using System;
using System.Text;

namespace Dharma.Common.Extensions
{
    /// <summary>
    /// Operações auxiliares para manipulação de valores em Base 64.
    /// </summary>
    public static class Base64Extension
    {
        /// <summary>
        /// Se um valor está em um formato de Base 64 válido.
        /// </summary>
        /// <param name="value">Valor que será verificado.</param>
        /// <returns>Retorna true se o valor for válido, senão, false.</returns>
        public static bool IsBase64(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            try
            {
                Encoding.UTF8.GetString(Convert.FromBase64String(value));
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Converte uma string para um valor em Base 64.
        /// </summary>
        /// <param name="value">Valor que será convertido.</param>
        /// <returns>Retorna o valor convertido para Base 64.</returns>
        public static string ToBase64(this string value)
        {
            if (value == null)
            {
                return null;
            }

            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Converte uma Base 64 para um valor em string.
        /// </summary>
        /// <param name="value">Valor que será convertido.</param>
        /// <returns>Retorna o valor convertido de Base 64 para um string pura.</returns>
        public static string FromBase64(this string value)
        {
            if (value == null)
            {
                return null;
            }

            var bytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
