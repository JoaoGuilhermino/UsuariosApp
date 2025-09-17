using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Interfaces.Security
{
    /// <summary>
    /// Inteface para operações de autenticação JWT
    /// </summary>
    public interface IJwtBearerSecurity 
    {
        /// <summary>
        /// Método para gerar um token JWT
        /// </summary>

        string GenerateToken(string user, string role);

    }
}
