using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoFaturamento.Core.Models.InputModels
{
    public class BaixaEstoqueInputModel(
        Guid ProdutoId, 
        int Saldo
    )
    {
        public Guid ProdutoId { get; } = ProdutoId;
        public int Saldo { get; } = Saldo;
    }
}
