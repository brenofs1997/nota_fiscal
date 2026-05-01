namespace ServicoEstoque.Core.Entities;

public class Produto
{
    public Produto(string codigo, string descricao, int saldo)
    {
        Id = Guid.NewGuid();
        Codigo = codigo;
        Descricao = descricao;
        Saldo = saldo;
    }

    public Guid Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public int Saldo { get; set; }

    public DateTime? CriadoEm { get; set; }

    public uint Versao { get; set; }

    public void SubtrairSaldo(int quantidade)
    {
        if (quantidade <= 0) return;

        if (Saldo < quantidade)
            throw new Exception($"Saldo insuficiente para o produto {Descricao}.");

        Saldo -= quantidade;
    }
}
