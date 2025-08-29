using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Infra.Data.Contexts;

namespace UsuariosApp.Infra.Data.Repositories
{
    public class PerfilRepository(DataContext dataContext) : IPerfilRepository
    {
        public Perfil? GetByNome(string nome)
        {
            return dataContext
                    .Set<Perfil>()
                    .Where(p => p.Nome.Equals(nome))
                    .SingleOrDefault();
        }
    }
}



