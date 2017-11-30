using System.Text.RegularExpressions;

namespace Dharma.Common.Extensions
{
    /// <summary>
    /// Operações auxiliares para tratar valores em Telefone Celular.
    /// </summary>
    public static class CellphoneNumberExtension
    {
        /// <summary>
        /// Valida um número de celular.
        /// </summary>
        /// <param name="cellphoneNumber">Nº de celular a ser validado.</param>
        /// <returns>Retorna true para um celular válido, senão false.</returns>
        public static bool IsCellphoneNumber(this string cellphoneNumber)
        {
            if (string.IsNullOrEmpty(cellphoneNumber))
            {
                return false;
            }

            try
            {
                var isValidMobile = Regex.IsMatch(cellphoneNumber,
                      @"[9]{0,1}[6-9]{1}[0-9]{3}[0-9]{5}$",
                      RegexOptions.IgnoreCase);
                return isValidMobile;
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
