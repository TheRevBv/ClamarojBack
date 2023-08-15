using System.Security.Cryptography;
using System.Text;

namespace ClamarojBack.Utils
{
    public class SecurityUtil
    {
        public string HashPassword(string password)
        {
            using SHA256 sha256Hash = SHA256.Create();
            // Convertir la contraseña en un arreglo de bytes
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convertir el arreglo de bytes a una cadena hexadecimal
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}

