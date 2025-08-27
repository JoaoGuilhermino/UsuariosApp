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
    public class PerfilPermissaoMap : IEntityTypeConfiguration<PerfilPermissao>
    {
        public void Configure(EntityTypeBuilder<PerfilPermissao> builder)
        {
            builder.ToTable("PERFIL_PERMISSAO"); //nome da tabela

            //chave primária composta
            builder.HasKey(p => new { p.PerfilId, p.PermissaoId });

            //mapeamento dos campos
            builder.Property(p => p.PerfilId).HasColumnName("PERFIL_ID").IsRequired();
            builder.Property(p => p.PermissaoId).HasColumnName("PERMISSAO_ID").IsRequired();

            //mapeamento do relacionamento (Perfil TEM MUITAS Permissões)
            builder.HasOne(p => p.Perfil)
                .WithMany(p => p.Permissoes)
                .HasForeignKey(p => p.PerfilId)
                .OnDelete(DeleteBehavior.Restrict);

            //mapeamento do relacionamento (Permissão TEM MUITOS Perfis)
            builder.HasOne(p => p.Permissao)
                .WithMany(p => p.Perfis)
                .HasForeignKey(p => p.PermissaoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}



