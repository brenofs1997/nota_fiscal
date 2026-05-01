using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServicoFaturamento.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoFaturamento.Infrastructure.Configurations
{
    public class ItensNotaFiscalConfiguration : IEntityTypeConfiguration<ItensNotaFiscal>
    {
        public void Configure(EntityTypeBuilder<ItensNotaFiscal> entity)
        {
            entity.ToTable("itens_nota_fiscal");
            entity.HasKey(e => e.Id).HasName("itens_nota_fiscal_pkey");
            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.NotaFiscalId).HasColumnName("nota_fiscal_id");
            entity.Property(e => e.ProdutoId).HasColumnName("produto_id");
            entity.Property(e => e.Quantidade).HasColumnName("quantidade");
            entity.HasOne<NotaFiscal>()
                .WithMany(n => n.Itens)
                .HasForeignKey(e => e.NotaFiscalId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
