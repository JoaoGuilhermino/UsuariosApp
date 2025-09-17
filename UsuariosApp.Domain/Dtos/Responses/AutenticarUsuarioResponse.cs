using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Dtos.Responses
{
    /// <summary>
    /// DTO para a saída de dados do serviço
    /// de autenticação de usuário no domínio.
    /// </summary>
    public record AutenticarUsuarioResponse(
            Guid Id, //Id do usuário
            string? Nome, //Nome do usuário
            string? Email, //Email do usuário
            string Perfil, //Nome do perfil
            DateTime DataHoraAcesso, //Data e hora de acesso
            string AccessToken //TOKEN JWT do usuário
        );
}




