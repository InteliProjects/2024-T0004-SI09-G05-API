using Microsoft.IdentityModel.Tokens;
using Polo.Dashboard.WebApi.Domain.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Polo.Dashboard.WebApi.Application.Services
{
    public class TokenService
    {
        public static object GenerateToken(EmpregadoDTO empregado, string role)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret);
            var claims = new Claim[]
            {
                new(ClaimTypes.NameIdentifier, empregado.n_pessoal.ToString()),
                new(ClaimTypes.Role, role),
                new(ClaimTypes.UserData, empregado.texto_rh) 
            };

            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new { token = tokenString };
        }

    }
}