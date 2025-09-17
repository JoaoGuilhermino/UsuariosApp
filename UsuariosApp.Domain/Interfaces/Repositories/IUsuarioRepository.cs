using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Método para adicionar um novo usuário no banco de dados.
        /// </summary>
        void Add(Usuario usuario);


        /// <summary>
        /// Método para verificar se um email já existe no Banco de dados
        /// </summary>
        bool VerifyEmailExists(string email);

        /// <summary>
        /// Método para obter 1 usuário pelo email e senha.
        /// </summary>
        Usuario? GetByEmailAndSenha(string email, string senha);

        /// <summary>
        /// Metodo para retornar 1 usuário pelo email.
        /// </summary>
        Usuario? GetByEmail(string email);  
    }
}
