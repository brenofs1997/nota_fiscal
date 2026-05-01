using Microsoft.EntityFrameworkCore;
using ServicoFaturamento.Core.Entities;
using ServicoFaturamento.Infrastructure.Configurations;
using System.Reflection;

namespace ServicoFaturamento.Infrastructure
{
    public class ServicoFaturamentoContext(DbContextOptions options) : DbContext(options)
    {
        public virtual DbSet<ItensNotaFiscal> ItensNotaFiscals { get; set; }

        public virtual DbSet<NotaFiscal> NotasFiscais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotaFiscal>(entity =>
            {
                entity.ToTable("nota_fiscal", "public");
                entity.HasKey(e => e.Id).HasName("nota_fiscal_pkey");
                entity.Property(e => e.Id).HasColumnName("id").HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(e => e.NumeroSequencial).HasColumnName("numero_sequencial").ValueGeneratedOnAdd();
                entity.Property(e => e.Status).HasColumnName("status");
                entity.Property(e => e.DataEmissao).HasColumnName("data_emissao");

                entity.HasMany(e => e.Itens).WithOne().HasForeignKey(e => e.NotaFiscalId);
                entity.Property(e => e.IdempotencyKey)
                      .HasColumnName("idempotency_key")
                      .IsRequired();

                entity.HasIndex(e => e.IdempotencyKey)
                      .IsUnique()
                      .HasDatabaseName("uk_nota_fiscal_idempotency");
            });

            modelBuilder.Entity<ItensNotaFiscal>(entity =>
            {
                entity.ToTable("itens_nota_fiscal", "public");
                entity.HasKey(e => e.Id).HasName("itens_nota_fiscal_pkey");
                entity.Property(e => e.Id).HasColumnName("id").HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(e => e.NotaFiscalId).HasColumnName("nota_fiscal_id");
                entity.Property(e => e.ProdutoId).HasColumnName("produto_id");
                entity.Property(e => e.Quantidade).HasColumnName("quantidade");
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}
