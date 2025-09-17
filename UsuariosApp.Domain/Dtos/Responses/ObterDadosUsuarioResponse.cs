using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Dtos.Responses
{
    /// <summary>
    /// DTO para a saída de dados do serviço
    /// de consulta de usuário.
    /// </summary>
    public record ObterDadosUsuarioResponse(
            Guid Id,        //Id do usuário
            string Nome,    //Nome do usuário
            string Email,   //Email do usuário
            string Perfil,  //Perfil do usuário
            List<PermissaoResponse> Permissoes  //Lista de permissões
        );

    public record PermissaoResponse(
            string Nome,    //Nome da permissão
            string Servico, //Serviço
            string Tipo     //Tipo
        );
}



