using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Interfaces.Security;
using UsuariosApp.Infra.Security.Settings;

namespace UsuariosApp.Infra.Security.Services
{
    /// <summary>
    /// Implementação para gerar o TOKEN JWT
    /// </summary>
    public class JwtBearerSecurity(JwtBearerSettings settings) : IJwtBearerSecurity
    {
        public string GenerateToken(string user, string role)
        {
            // Gerar a chave secreta para assinar o TOKEN JWT (assinatura antifalsificação)
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims (informações do usuário)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user), //Nome do usuário
                new Claim(ClaimTypes.Role, role) //Perfil do usuário
            };

            //Criação do TOKEN JWT
            var token = new JwtSecurityToken(
                issuer: settings.Issuer, //Emissor
                audience: settings.Audience, //Destinatário
                claims: claims, //Informações do usuário
                notBefore: DateTime.UtcNow, //Data de geração do token
                expires: DateTime.UtcNow.AddMinutes(settings.ExpirationInMinutes), //Data de expiração
                signingCredentials: credentials //Chave de assinatura
                );

            //Retornando o TOKEN JWT
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}



