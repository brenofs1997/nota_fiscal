using ServicoFaturamento.Core.Entities;
using ServicoFaturamento.Core.Models.ViewModel;

namespace ServicoFaturamento.Core.Mappers
{
    public static class NotaFiscalMapper
    {
        public static NotaFiscalViewModel ToViewModel(this NotaFiscal notaFiscal)
        {
            return new NotaFiscalViewModel(
                notaFiscal.Id,
                notaFiscal.NumeroSequencial,    
                notaFiscal.Status,
                notaFiscal.DataEmissao,
                notaFiscal.Itens.Select(i => new NotaFiscalItemViewModel(i.Id, i.ProdutoId, i.Quantidade))
            );
        }
    }
}
