using MediatR;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi.Queries.PlanoDeAcao
{
    internal class ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQueryHandler : IRequestHandler<ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQuery, ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQueryResult>
    {
        private readonly PlanosDeAcaoQueryContext planosDeAcaoQueryContext;

        public ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQueryHandler(PlanosDeAcaoQueryContext planosDeAcaoQueryContext)
        {
            this.planosDeAcaoQueryContext = planosDeAcaoQueryContext ?? throw new ArgumentNullException(nameof(planosDeAcaoQueryContext));
        }

        public async Task<ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQueryResult> Handle(ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQuery request, CancellationToken cancellationToken)
        {
            var query = $@"
                select
                    i.[Id], i.[Codigo], i.[Descricao], i.[Acao], i.[Prazo], i.[Status], i.[PlanoDeAcaoId]
                from
                    [{planosDeAcaoQueryContext.DatabaseName}]..[Itens] i with(nolock)
                inner join
                    [{planosDeAcaoQueryContext.DatabaseName}]..[Responsaveis] r with(nolock)
                    on i.[Id] = r.[ItemId]
                where
                    i.[Status] not in @statusItemPlanoDeAcao and r.[Email] = @email
                order by
					i.[Prazo]";

            var queryResult = new ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQueryResult()
            {
                Itens = await planosDeAcaoQueryContext.QueryAsync<ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQueryResult.ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQueryResultInnerItem>(query, new
                {
                    statusItemPlanoDeAcao = new[] { StatusAgendamento.Realizado, StatusAgendamento.Cancelado },
                    request.Email
                })
            };

            return queryResult;
        }
    }
}