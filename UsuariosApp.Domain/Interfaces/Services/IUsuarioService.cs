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
    /// Interface de serviço para operações relacionadas a usuários.
    /// </summary>
    public interface IUsuarioService
    {
        CriarUsuarioResponse CriarUsuario(CriarUsuarioRequest request);

    }
}
