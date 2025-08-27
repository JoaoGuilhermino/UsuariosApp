using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Infra.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO"); //Nome da tabela

            builder.HasKey(u => u.Id); //Chave primária

            //mapeamento das propriedades
            builder.Property(u => u.Id).HasColumnName("ID");
            builder.Property(u => u.Nome).HasColumnName("NOME").HasMaxLength(150).IsRequired();
            builder.Property(u => u.Email).HasColumnName("EMAIL").HasMaxLength(50).IsRequired();
            builder.Property(u => u.Senha).HasColumnName("SENHA").HasMaxLength(100).IsRequired();
            builder.Property(u => u.PerfilId).HasColumnName("PERFIL_ID").IsRequired();

            //mapeamento dos índices
            builder.HasIndex(u => u.Email).IsUnique();

            //mapeamento os relacionamentos
            builder.HasOne(u => u.Perfil) //Usuário TEM 1 Perfil
                .WithMany(p => p.Usuarios) //Perfil TEM MUITOS Usuarios
                .HasForeignKey(u => u.PerfilId) //Chave estrangeira
                .OnDelete(DeleteBehavior.Restrict); //Regra de exclusão
        }
    }
}



