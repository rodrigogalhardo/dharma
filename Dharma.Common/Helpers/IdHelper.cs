using System;
using System.Linq;

namespace Dharma.Common.Helpers
{
    /// <summary>
    /// Gerador de identificadores.
    /// </summary>
    public class IdHelper
    {
        private static Random random = new Random();

        /// <summary>
        /// Gera um identificador com o tamanho solicitado.
        /// </summary>
        /// <returns>Identificador com o tamanho solicitado.</returns>
        public static string Make(int size = 24)
        {
            if (size <= 0 || size > 48)
            {
                throw new ArgumentOutOfRangeException("size", "O parâmetro size não pode ser menos do que 1 e mais do que 48.");
            }

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, size)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
