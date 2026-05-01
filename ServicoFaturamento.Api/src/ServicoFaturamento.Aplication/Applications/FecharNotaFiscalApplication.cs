using Microsoft.Extensions.Logging;
using ServicoFaturamento.Application.Notifications;
using ServicoFaturamento.Core.Interfaces.Applications;
using ServicoFaturamento.Core.Interfaces.Notifications;
using ServicoFaturamento.Core.Interfaces.Repositories;
using ServicoFaturamento.Core.Interfaces.Services;
using ServicoFaturamento.Core.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ServicoFaturamento.Application.Applications
{
    public class FecharNotaFiscalApplication(
         INotaFiscalRepository notaFiscalRepository,
         IEstoqueIntegrationService estoqueService,
         INotifier notifier,
         ILogger<FecharNotaFiscalApplication> logger
     ) : IFecharNotaFiscalApplication
    {
        public async Task FecharAsync(Guid id)
        {
            try
            {
                logger.LogInformation("Iniciando processo de fechamento da Nota Fiscal: {NotaId}", id);

                var notaFiscal = await notaFiscalRepository.BuscarPorIdAsync(id);

                if (notaFiscal == null)
                {
                    logger.LogWarning("Falha ao fechar nota: Nota Fiscal {NotaId} não encontrada.", id);
                    notifier.Handle($"A Nota Fiscal com ID {id} não foi localizada no sistema.", HttpStatusCode.NotFound);
                    return;
                }


                if (!notaFiscal.Status.Equals("Aberta", StringComparison.OrdinalIgnoreCase))
                {
                    logger.LogWarning("Tentativa de fechar nota {NotaId} com status inválido: {Status}", id, notaFiscal.Status);
                    notifier.Handle($"Não é possível fechar a nota nº {notaFiscal.Id}. O status atual é '{notaFiscal.Status}', mas a nota precisa estar 'Aberta'.", HttpStatusCode.BadRequest);
                    return;
                }

                logger.LogInformation("Solicitando baixa de estoque para {ItemCount} itens da nota {NotaId}", notaFiscal.Itens.Count, id);

                var itensParaBaixa = notaFiscal.Itens.Select(i =>
                    new BaixaEstoqueInputModel(i.ProdutoId, i.Quantidade)
                ).ToList();


                var sucessoEstoque = await estoqueService.AtualizarSaldoProdutosAsync(itensParaBaixa);

                if (!sucessoEstoque)
                {
                    logger.LogError("O Serviço de Estoque recusou a baixa para a nota {NotaId}", id);
                    notifier.Handle("Falha na integração: O Serviço de Estoque rejeitou a atualização do saldo ou está indisponível.", HttpStatusCode.UnprocessableEntity);
                    return;
                }

                notaFiscal.Status = "Fechada";

                await notaFiscalRepository.SaveChangesAsync();
                logger.LogInformation("Nota Fiscal {NotaId} fechada com sucesso e estoque atualizado.", id);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Erro fatal ao fechar a Nota Fiscal {NotaId}", id);
                notifier.Handle(ex.Message, HttpStatusCode.BadRequest);
                return;
            }
        }
    }
}
