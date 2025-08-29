using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Interfaces.Repositories
{
    public interface IPerfilRepository
    {
        /// <summary>
        /// Método para obter 1 perfil pelo nome.
        /// </summary>
        Perfil? GetByNome(string nome);
    }
}
