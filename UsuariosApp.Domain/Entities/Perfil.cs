using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Entities
{
    public class Perfil
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;

        #region Relacionamentos

        public ICollection<Usuario>? Usuarios { get; set; }
        public ICollection<PerfilPermissao>? Permissoes { get; set; }

        #endregion

    }
}
