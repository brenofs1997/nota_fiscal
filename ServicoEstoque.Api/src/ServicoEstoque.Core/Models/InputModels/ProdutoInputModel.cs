using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoEstoque.Core.Models.InputModels
{
    public class ProdutoInputModel(
        string codigo,
        string descricao,
        int saldo
        ) : BaseProdutoInputModel(codigo, descricao, saldo)
    {
        public string Codigo { get; } = codigo!;

        public string Descricao { get; } = descricao!;

        public int Saldo { get; } = saldo;
    }
}
