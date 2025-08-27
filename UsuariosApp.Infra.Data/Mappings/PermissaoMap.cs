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
    public class PermissaoMap : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(EntityTypeBuilder<Permissao> builder)
        {
            builder.ToTable("PERMISSAO"); //nome da tabela

            builder.HasKey(p => p.Id); //chave primária

            //mapeamento dos campos
            builder.Property(p => p.Id).HasColumnName("ID");
            builder.Property(p => p.Nome).HasColumnName("NOME").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Servico).HasColumnName("SERVICO").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Tipo).HasColumnName("TIPO").HasMaxLength(10).IsRequired();

            //mapeamento dos índices
            builder.HasIndex(p => p.Nome).IsUnique();
        }
    }
}



