using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Dharma.Common.Extensions
{
    /// <summary>
    /// Operações auxiliares para manipulação de e-mails.
    /// </summary>
    public static class EmailExtension
    {
        /// <summary>
        /// Verifica se um e-mail está no formato válido.
        /// </summary>
        /// <param name="value">E-mail que será verificado.</param>
        /// <returns>Retorna true se o e-mail for válido, senão, false.</returns>
        public static bool IsEmail(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            try
            {
                value = Regex.Replace(value, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (Exception)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(value,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static string DomainMapper(Match match)
        {
            var idn = new IdnMapping();

            string domainName = match.Groups[2].Value;

            domainName = idn.GetAscii(domainName);

            return match.Groups[1].Value + domainName;
        }
    }
}
