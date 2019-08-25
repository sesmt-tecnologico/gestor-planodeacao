using Furiza.Base.Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SESMTTech.Gestor.PlanosDeAcao.Infrastructure.EntityFrameworkCore.Repositories
{
    internal class PlanoDeAcaoRepository : IPlanoDeAcaoReadOnlyRepository, IPlanoDeAcaoWriteOnlyRepository
    {
        private readonly PlanosDeAcaoContext coreBusinessContext;

        public IUnitOfWork UnitOfWork => coreBusinessContext;

        public PlanoDeAcaoRepository(PlanosDeAcaoContext coreBusinessContext)
        {
            this.coreBusinessContext = coreBusinessContext ?? throw new ArgumentNullException(nameof(coreBusinessContext));
        }

        public PlanoDeAcao GetById(Guid id)
        {
            var planoDeAcao = coreBusinessContext.Set<PlanoDeAcao>()
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Responsaveis)
                .SingleOrDefault(p => p.Id == id);

            return planoDeAcao;
        }

        public async Task<PlanoDeAcao> GetByIdAsync(Guid id)
        {
            var planoDeAcao = await coreBusinessContext.Set<PlanoDeAcao>()
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Responsaveis)
                .SingleOrDefaultAsync(p => p.Id == id);

            return planoDeAcao;
        }

        public async Task<int> GetNumeroDoUltimoPlanoDeAcaoAsync(int ano)
        {
            var planoDeAcao = await coreBusinessContext.Set<PlanoDeAcao>()
                .AsNoTracking()
                .Where(p => p.Ano == ano)
                .OrderByDescending(p => p.Numero)
                .FirstOrDefaultAsync();

            return planoDeAcao?.Numero ?? 0;
        }

        public void BatchInsert(IEnumerable<PlanoDeAcao> aggregates)
        {
            coreBusinessContext.Set<PlanoDeAcao>().AddRange(aggregates);
        }

        public void BatchUpdate(IEnumerable<PlanoDeAcao> aggregates)
        {
            coreBusinessContext.Set<PlanoDeAcao>().UpdateRange(aggregates);
        }

        public void Insert(PlanoDeAcao aggregate)
        {
            coreBusinessContext.Set<PlanoDeAcao>().Add(aggregate);
        }

        public void Update(PlanoDeAcao aggregate)
        {
            coreBusinessContext.Set<PlanoDeAcao>().Update(aggregate);
        }
    }
}