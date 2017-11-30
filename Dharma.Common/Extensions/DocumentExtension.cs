namespace Dharma.Common.Extensions
{
    /// <summary>
    /// Operações auxiliares para validação de documento CPF ou CNPJ.
    /// </summary>
    public static class DocumentExtension
    {
        private static int[] multiplicadorCnpj1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static int[] multiplicadorCnpj2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static int[] multiplicadorCpf1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static int[] multiplicadorCpf2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        /// <summary>
        /// Verifica se um CPF ou CNPJ em um formato válido.
        /// </summary>
        /// <param name="value">Documento que será verificado.</param>
        /// <returns>Retorna true se o documento for válido, senão, false.</returns>
        public static bool IsDocument(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (!IsValidCpf(value) && !IsValidCnpj(value))
            {
                return false;
            }

            return true;
        }

        private static bool IsValidCnpj(string cnpj)
        {
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
            {
                return false;
            }

            string tempCnpj = cnpj.Substring(0, 12);

            int soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicadorCnpj1[i];
            }

            int resto = (soma % 11);

            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicadorCnpj2[i];
            }

            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        private static bool IsValidCpf(string cpf)
        {
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", string.Empty).Replace("-", string.Empty);

            if (cpf.Length != 11)
            {
                return false;
            }

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadorCpf1[i];
            }

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadorCpf2[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
