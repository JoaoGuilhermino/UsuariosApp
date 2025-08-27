using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Entities
{
    public class Permissao
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Servico { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;

        #region Relacionamentos

        public ICollection<PerfilPermissao>? Perfis { get; set; }

        #endregion
    }
}
