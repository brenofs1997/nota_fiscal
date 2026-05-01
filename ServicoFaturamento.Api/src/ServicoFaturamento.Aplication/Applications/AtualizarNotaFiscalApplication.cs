using Microsoft.EntityFrameworkCore;
using ServicoFaturamento.Application.Notifications;
using ServicoFaturamento.Core.Entities;
using ServicoFaturamento.Core.Interfaces.Applications;
using ServicoFaturamento.Core.Interfaces.Repositories;
using ServicoFaturamento.Core.Models.InputModels;

namespace ServicoFaturamento.Application.Applications
{
    public class AtualizarNotaFiscalApplication(
        INotaFiscalRepository notaFiscalRepository
    ) : IAtualizarNotaFiscalApplication
    {
        public async Task AtualizarAsync(Guid id, AtualizacaoNotaFiscalInputModel inputModel)
        {
            // 1. Busca a nota COM os itens para o Tracker saber quem são os antigos
            var notaFiscal = await notaFiscalRepository.BuscarPorIdAsync(id);
            if (notaFiscal == null)
                throw new KeyNotFoundException("Nota Fiscal não encontrada.");

            // 2. Criamos uma lista auxiliar para evitar erro de "coleção modificada"
            var itensAntigos = notaFiscal.Itens.ToList();

            // 3. REMOÇÃO REAL
            foreach (var item in itensAntigos)
            {
                // Remove da lista em memória
                notaFiscal.Itens.Remove(item);

                // FORÇA o estado 'Deleted' no ChangeTracker
                notaFiscalRepository.RemoverItemFisico(item);
            }

            // 4. ADIÇÃO DE NOVOS
            foreach (var itemInput in inputModel.Itens)
            {
                // Criamos objetos totalmente novos (Novos IDs)
                notaFiscal.Itens.Add(new ItensNotaFiscal(
                    itemInput.ProdutoId,
                    itemInput.Quantidade,
                    id // Vincula ao NotaFiscalId
                ));
            }

            try
            {
                await notaFiscalRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Erro de concorrência!");
                throw;
            }
        }
    }
}
