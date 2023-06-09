using Application.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils
{
    public static class TokenAuthentication
    {
        public static string GenerateToken(AuthModel authModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);

            var claims = new List<Claim>
            {
                new Claim("id", authModel.id.ToString()),
                new Claim("NomeCompleto", authModel.nomeCompleto.ToString()),
                new Claim("Email", authModel.email.ToString()),
                new Claim("Senha", authModel.senha.ToString()),
                new Claim("Ip", authModel.ip.ToString()),
            };

            if (authModel.coordenador)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Coordenador"));
            }
            if (authModel.professor)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Professor"));
            }
            if (authModel.orientador)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Orientador"));
            }
            if (authModel.aluno)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Aluno"));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(31),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static AuthModel GetTokenAuthModel(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var claims = handler.ReadJwtToken(token).Claims.ToArray();
            var jwtModel = new AuthModel
            {
                id = int.Parse(claims[0].Value),
                nomeCompleto = claims[1].Value,
                email = claims[2].Value,
                senha = claims[3].Value,
                ip = claims[4].Value,
                aluno = claims.Any(c => c.Value == "Aluno"),
                professor = claims.Any(c => c.Value == "Professor"),
                coordenador = claims.Any(c => c.Value == "Coordenador"),
                orientador = claims.Any(c => c.Value == "Orientador")
            };

            return jwtModel;
        }
    }
}
