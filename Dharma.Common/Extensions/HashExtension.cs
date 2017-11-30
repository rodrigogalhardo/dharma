namespace Dharma.Common.Extensions
{
    /// <summary>
    /// Operações auxiliares para manipulação de hashs.
    /// </summary>
    public static class HashExtension
    {
        /// <summary>
        /// Verifica se um hash é válido.
        /// </summary>
        /// <param name="value">Hash que será verificado.</param>
        /// <returns>Retorna true se o hash for válido, senão, false.</returns>
        public static bool IsHash(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (value.Length != 24)
            {
                return false;
            }

            foreach (var item in value)
            {
                if (!char.IsLetterOrDigit(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
