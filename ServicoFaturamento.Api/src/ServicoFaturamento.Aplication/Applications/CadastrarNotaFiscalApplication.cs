using Microsoft.Extensions.Logging;
using ServicoFaturamento.Application.Notifications;
using ServicoFaturamento.Core.Entities;
using ServicoFaturamento.Core.Interfaces.Applications;
using ServicoFaturamento.Core.Interfaces.Notifications;
using ServicoFaturamento.Core.Interfaces.Repositories;
using ServicoFaturamento.Core.Models.InputModels;
using System.Net;

namespace ServicoFaturamento.Application.Applications
{
    public class CadastrarNotaFiscalApplication(
        IUnitOfWork unitOfWork,
        INotifier notifier,
        ILogger<CadastrarNotaFiscalApplication> logger
    ) : ICadastrarNotaFiscalApplication
    {
        public async Task<Guid> CadastrarAsync(NotaFiscalInputModel inputModel)
        {
            try
            {
                logger.LogInformation("Iniciando cadastro de Nota Fiscal. IdempotencyKey: {Key}", inputModel.IdempotencyKey);

                if (inputModel.Itens == null || !inputModel.Itens.Any())
                {
                    logger.LogWarning("Tentativa de cadastro de nota sem itens. IdempotencyKey: {Key}", inputModel.IdempotencyKey);
                    notifier.Handle("A nota fiscal deve conter pelo menos um produto.",HttpStatusCode.BadRequest);
                    return Guid.Empty;
                }

                var notaExistente = await unitOfWork.NotasFiscais.ObterPorChaveIdempotenciaAsync(inputModel.IdempotencyKey);

                if (notaExistente != null)
                {
                    logger.LogInformation("Nota Fiscal já processada anteriormente (Idempotência). ID: {NotaId}", notaExistente.Id);
                    return notaExistente.Id;
                }

                var notaFiscal = new NotaFiscal
                {
                    IdempotencyKey = inputModel.IdempotencyKey,
                    Status = "Aberta",
                    DataEmissao = DateTime.UtcNow,
                    Itens = inputModel.Itens.Select(item => new ItensNotaFiscal
                    {
                        ProdutoId = item.ProdutoId,
                        Quantidade = item.Quantidade
                    }).ToList()
                };



                await unitOfWork.BeginTransactionAsync();

                var notaFiscalId = await unitOfWork.NotasFiscais.CadastrarAsync(notaFiscal);

                await unitOfWork.CommitAsync();

                logger.LogInformation("Nota Fiscal cadastrada com sucesso. ID Gerado: {NotaId}", notaFiscalId);

                return notaFiscalId;
            }
            catch (Exception)
            {
                notifier.Handle("Ocorreu um erro interno ao processar a nota fiscal.", HttpStatusCode.InternalServerError);
                return Guid.Empty;
            }
        }
    }
}
