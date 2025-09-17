using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Infra.Security.Settings
{
    /// <summary>
    /// Classe para capturar as configurações 
    /// necessárias para geração do TOKEN JWT.
    /// </summary>
    public class JwtBearerSettings
    {
        public string? Issuer { get; set; } //Emissor do TOKEN
        public string? Audience { get; set; } //Destinatário do TOKEN
        public string? SecretKey { get; set; } //Chave de assinatura
        public int ExpirationInMinutes { get; set; } //Tempo de expiração
    }
}



