namespace Dharma.Common.Helpers
{
    /// <summary>
    /// Operações para auxiliar na manipulação de numéricos.
    /// </summary>
    public static class NumberHelper
    {
        /// <summary>
        /// Se a string informada é um inteiro.
        /// </summary>
        /// <param name="number">Valor que será verificado.</param>
        /// <returns>Retorna true se a string informada for inteiro, senão, false.</returns>
        public static bool IsInteger(this string number)
        {
            int outValue = 0;

            return int.TryParse(number, out outValue);
        }
    }
}
