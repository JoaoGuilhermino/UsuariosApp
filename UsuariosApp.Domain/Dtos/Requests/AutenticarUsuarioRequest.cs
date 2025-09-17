using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Dtos.Requests
{
    /// <summary>
    /// DTO para a entrada de dados da requisição
    /// de autenticação de usuário do serviço de domínio.
    /// </summary>
    public record AutenticarUsuarioRequest(
        string? Email,  //Email de acesso
        string? Senha   //Senha de acesso
        );
}



