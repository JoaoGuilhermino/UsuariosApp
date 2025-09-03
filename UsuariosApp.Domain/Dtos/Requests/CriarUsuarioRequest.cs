using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Dtos.Requests
{
    /// <summary>
    /// DTO para a entrada de dados da requisição
    /// de criação de usuário do serviço de domínio.
    /// </summary>
    public record CriarUsuarioRequest(
        string? Nome,    //Nome do usuário
        string? Email,   //Endereço de email do usuário
        string? Senha    //Senha do usuário
        );
}



