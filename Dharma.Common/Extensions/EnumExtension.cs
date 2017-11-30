using System;
using System.ComponentModel;

namespace Dharma.Common.Extensions
{
    /// <summary>
    /// Operações auxiliares para manipulação de enumeradores.
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Obtém o valor do atributo 'Description' decorado em um valor de um enumerador.
        /// </summary>
        /// <param name="value">Item do enumerador.</param>
        /// <returns>Descrição do enumerador.</returns>
        public static string Description(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes == null || attributes.Length == 0)
            {
                return value.ToString();
            }

            return attributes[0].Description;
        }

        /// <summary>
        /// Obtém o valor do atributo 'Description' decorado em um valor de um enumerador.
        /// </summary>
        /// <param name="value">Item do enumerador.</param>
        /// <param name="parameters">Parâmetros do erro.</param>
        /// <returns>Descrição do enumerador.</returns>
        public static string Description(this Enum value, params string[] parameters)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes == null || attributes.Length == 0)
            {
                return string.Format(value.ToString(), parameters);
            }

            return string.Format(attributes[0].Description, parameters);
        }
    }
}
