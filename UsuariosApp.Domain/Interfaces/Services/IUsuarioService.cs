using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Dtos.Requests;
using UsuariosApp.Domain.Dtos.Responses;

namespace UsuariosApp.Domain.Interfaces.Services
{
    /// <summary>
    /// Interface de serviços de dominio para usuário.
    /// </summary>
    public interface IUsuarioService
    {
        CriarUsuarioResponse CriarUsuario(CriarUsuarioRequest request);

        AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request);

        ObterDadosUsuarioResponse ObterDadosUsuario(string email);
    }
}



