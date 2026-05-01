namespace ServicoFaturamento.Core.Models.ViewModel
{
    public class NotaFiscalViewModel(
         Guid id,
         int numeroSequencial,
         string status,
         DateTime? dataEmissao,
         IEnumerable<NotaFiscalItemViewModel> itens)
    {
        public Guid Id { get; } = id;
        public int NumeroSequencial { get; } = numeroSequencial;
        public string Status { get; } = status;
        public DateTime? DataEmissao { get; } = dataEmissao;
        public IEnumerable<NotaFiscalItemViewModel> Itens { get; } = itens;
    }
}
