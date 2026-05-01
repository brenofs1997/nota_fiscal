namespace ServicoFaturamento.Core.Entities;

public class NotaFiscal
{
    public Guid Id { get; set; }

    public int NumeroSequencial { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? DataEmissao { get; set; }

    public ICollection<ItensNotaFiscal> Itens { get; set; } = new List<ItensNotaFiscal>();

    public Guid IdempotencyKey { get; set; }
}
