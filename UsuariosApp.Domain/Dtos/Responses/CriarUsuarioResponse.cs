using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Dtos.Responses
{
    /// <summary>
    /// DTO para a saída de dados do serviço
    /// de criação de usuário no domínio.
    /// </summary>
    public record CriarUsuarioResponse(
            Guid Id, //Id do usuário
            string? Nome, //Nome do usuário
            string? Email, //Email do usuário
            DateTime DataHoraCriacao, //Data e hora de criação
            string Perfil //Nome do perfil
        );
}


