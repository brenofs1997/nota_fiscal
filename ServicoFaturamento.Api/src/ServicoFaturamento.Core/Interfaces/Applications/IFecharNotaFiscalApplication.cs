namespace ServicoFaturamento.Core.Interfaces.Applications
{
    public interface IFecharNotaFiscalApplication
    {
        Task FecharAsync(Guid id);
    }
}
