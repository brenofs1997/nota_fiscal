using ServicoEstoque.Core.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoEstoque.Core.Interfaces.Applications
{
    public interface IDeletarProdutoApplication
    {
        Task Deletar(Guid id);
    }
}
