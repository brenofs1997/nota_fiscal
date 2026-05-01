using ServicoFaturamento.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServicoFaturamento.Infrastructure.Configurations
{
    public class NotaFiscalConfiguration : IEntityTypeConfiguration<NotaFiscal>
    {
        public void Configure(EntityTypeBuilder<NotaFiscal> entity)
        {
            entity.ToTable("nota_fiscal", "public");
            entity.HasKey(e => e.Id).HasName("nota_fiscal_pkey");
            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.NumeroSequencial)
                .HasColumnName("numero_sequencial")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.DataEmissao)
                .HasColumnName("data_emissao");
            entity.HasMany(e => e.Itens)
              .WithOne()
              .HasForeignKey(e => e.NotaFiscalId);

            entity.Property(e => e.IdempotencyKey)
                .HasColumnName("idempotency_key")
                .IsRequired();

            entity.HasIndex(e => e.IdempotencyKey)
                  .IsUnique()
                  .HasDatabaseName("uk_nota_fiscal_idempotency");


        }
    }
}
