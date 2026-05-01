namespace ServicoFaturamento.Core.Entities;

public  class ItensNotaFiscal
{
    public Guid Id { get; private set; }

    public Guid NotaFiscalId { get; set; }

    public Guid ProdutoId { get; set; }

    public int Quantidade { get; set; }

    public ItensNotaFiscal()
    {
        
    }

    public ItensNotaFiscal(Guid produtoId, int quantidade)
    {
        ProdutoId = produtoId;
        Quantidade = quantidade;
    }

    public ItensNotaFiscal(Guid produtoId, int quantidade, Guid notaFiscalId)
    {
        Id = Guid.NewGuid();
        ProdutoId = produtoId;
        Quantidade = quantidade;
        NotaFiscalId = notaFiscalId;
    }

    public void AtualizarQuantidade(int novaQuantidade)
    {
        if (novaQuantidade <= 0) throw new Exception("Quantidade inválida");
        this.Quantidade = novaQuantidade;
    }
}
