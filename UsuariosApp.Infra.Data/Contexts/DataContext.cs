using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Infra.Data.Mappings;

namespace UsuariosApp.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        //Método construtor
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        


        //Método para adicionar as classes de mapeamento
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new PermissaoMap());
            modelBuilder.ApplyConfiguration(new PerfilPermissaoMap());
        }
    }
}



