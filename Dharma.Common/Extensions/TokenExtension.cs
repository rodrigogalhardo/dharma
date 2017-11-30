using System;
using System.Text.RegularExpressions;

namespace Dharma.Common.Extensions
{
    /// <summary>
    /// Operações auxiliares para manipulação de tokens.
    /// </summary>
    public static class TokenExtension
    {
        /// <summary>
        /// Retorna true se for um token no formato GUID, senão, false.
        /// </summary>
        /// <param name="token">Token que será verificado.</param>
        /// <returns>Retorna true se for um token no formato GUID, senão, false.</returns>
        public static bool IsGuid(this string token)
        {
            Guid parsed = Guid.Empty;

            return Guid.TryParse(token, out parsed);
        }

        /// <summary>
        /// Retorna true se for um token SMS, senão, false.
        /// </summary>
        /// <param name="token">Token que será verificado.</param>
        /// <returns>Retorna true se for um token SMS, senão, false.</returns>
        public static bool IsSmsToken(this string token)
        {
            return Regex.IsMatch(token, @"^\d{6}$");
        }
    }
}
