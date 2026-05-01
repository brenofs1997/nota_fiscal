namespace ServicoFaturamento.Core.Models.InputModels
{
    public class AtualizacaoNotaFiscalInputModel(
        IEnumerable<NotaFiscalItemInputModel> itens
    )
    {
        public IEnumerable<NotaFiscalItemInputModel> Itens { get; } = itens;
    }
}
