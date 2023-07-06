using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ApiWithAuth
{
    public class TokenService
    {
        private const int ExpirationMinutes = 30;
        
        public string CreateToken(IdentityUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);

            var token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCredentials(),
                expiration
            );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
            DateTime expiration) =>
            new(
                "apiWithAuthBackend",
                "apiWithAuthBackend",
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

        private List<Claim> CreateClaims(IdentityUser user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "TokenForTheApiWithAuth"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };
                return claims;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(GenerateRandomPrivateKey())
                ),
                SecurityAlgorithms.HmacSha256
            );
        }

        private string GenerateRandomPrivateKey()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                // Definir la longitud en bytes necesaria para obtener al menos 256 bits
                int byteLength = 32; // 256 bits / 8 bits por byte = 32 bytes

                byte[] randomBytes = new byte[byteLength];
                rng.GetBytes(randomBytes);

                // Convertir los bytes aleatorios en una cadena hexadecimal
                string privateKey = BitConverter.ToString(randomBytes).Replace("-", "");

                return privateKey;
            }
        }

    }
}