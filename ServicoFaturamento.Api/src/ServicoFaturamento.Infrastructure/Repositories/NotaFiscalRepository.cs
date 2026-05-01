using ServicoFaturamento.Core.Entities;
using ServicoFaturamento.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ServicoFaturamento.Infrastructure.Repositories
{
    public class NotaFiscalRepository(ServicoFaturamentoContext dbContext) : INotaFiscalRepository
    {
        public async Task<Guid> CadastrarAsync(NotaFiscal notaFiscal)
        {
            var entidade = await dbContext.NotasFiscais.AddAsync(notaFiscal);

            await dbContext.SaveChangesAsync();

            return entidade.Entity.Id;
        }

        public async Task<NotaFiscal?> ObterPorChaveIdempotenciaAsync(Guid idempotencyKey)
        {
            var notaFiscal = await dbContext.NotasFiscais.FirstOrDefaultAsync(c => c.IdempotencyKey == idempotencyKey);

            return notaFiscal;
        }

        public async Task<IEnumerable<NotaFiscal>> BuscarAsync()
        {
            var notasFiscais = await dbContext.NotasFiscais.Include(n => n.Itens).ToListAsync(); ;

            return notasFiscais;
        }

        public async Task<NotaFiscal?> BuscarPorIdAsync(Guid id)
        {
            var notaFiscal = await dbContext.NotasFiscais.Include(n => n.Itens).FirstOrDefaultAsync(c => c.Id == id);

            return notaFiscal;
        }

        public Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }



    }
}
