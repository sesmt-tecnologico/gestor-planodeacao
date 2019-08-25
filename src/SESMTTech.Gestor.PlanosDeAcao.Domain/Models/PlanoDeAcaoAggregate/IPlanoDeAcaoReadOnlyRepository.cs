using Furiza.Base.Core.SeedWork;
using System.Threading.Tasks;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate
{
    public interface IPlanoDeAcaoReadOnlyRepository : IAggregateReadOnlyRepository<PlanoDeAcao>
    {
        Task<int> GetNumeroDoUltimoPlanoDeAcaoAsync(int ano);
    }
}