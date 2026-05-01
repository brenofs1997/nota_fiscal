using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicoEstoque.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoEstoque.Infrastructure.Configurations
{
    public class ProdutoConfigurations : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> entity)
        {
            entity.HasKey(e => e.Id).HasName("produtos_pkey");

            entity.ToTable("produtos");

            entity.HasIndex(e => e.Codigo, "produtos_codigo_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .HasColumnName("codigo");
            entity.Property(e => e.CriadoEm)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("criado_em");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .HasColumnName("descricao");
            entity.Property(e => e.Saldo).HasColumnName("saldo");
            entity.Property(p => p.Versao)
                   .IsRowVersion();
        }
    }
}
