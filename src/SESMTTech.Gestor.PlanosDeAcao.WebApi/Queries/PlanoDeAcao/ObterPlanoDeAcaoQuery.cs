using MediatR;
using System;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi.Queries.PlanoDeAcao
{
    public class ObterPlanoDeAcaoQuery : IRequest<ObterPlanoDeAcaoQueryResult>
    {
        public Guid PlanoDeAcaoId { get; set; }
    }
}