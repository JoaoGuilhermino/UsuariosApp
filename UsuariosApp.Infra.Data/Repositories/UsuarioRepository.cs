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
    public class UsuarioRepository(DataContext dataContext) : IUsuarioRepository
    {

        public void Add(Usuario usuario)
        {
            dataContext.Add(usuario);
            dataContext.SaveChanges();
        }

        public bool VerifyEmailExists(string email)
        {
            return dataContext
                    .Set<Usuario>()
                    .Any(u => u.Email.Equals(email));
        }

        public Usuario? GetByEmailAndSenha(string email, string senha)
        {
            return dataContext
                    .Set<Usuario>()
                    .Where(u => u.Email.Equals(email)
                             && u.Senha.Equals(senha))
                    .SingleOrDefault();
        }

    }
}



