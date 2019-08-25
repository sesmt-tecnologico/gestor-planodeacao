using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi.Queries.PlanoDeAcao
{
    internal class ObterPlanoDeAcaoQueryHandler : IRequestHandler<ObterPlanoDeAcaoQuery, ObterPlanoDeAcaoQueryResult>
    {
        private readonly PlanosDeAcaoQueryContext planosDeAcaoQueryContext;

        public ObterPlanoDeAcaoQueryHandler(PlanosDeAcaoQueryContext planosDeAcaoQueryContext)
        {
            this.planosDeAcaoQueryContext = planosDeAcaoQueryContext ?? throw new ArgumentNullException(nameof(planosDeAcaoQueryContext));
        }

        public async Task<ObterPlanoDeAcaoQueryResult> Handle(ObterPlanoDeAcaoQuery request, CancellationToken cancellationToken)
        {
            var query = $@"
                select
                    pa.*, 
                    i.[Id] ItemId, i.[Codigo], i.[Descricao], i.[Acao], i.[Prazo], i.[Status], i.[DataRealizacao],
                    r.[Id] ResponsavelId, r.[NomeCompleto], r.[Email]
                from
                    [{planosDeAcaoQueryContext.DatabaseName}]..[PlanosDeAcao] pa with(nolock)
                left join
                    [{planosDeAcaoQueryContext.DatabaseName}]..[Itens] i with(nolock)
                    on pa.[Id] = i.[PlanoDeAcaoId]
                left join
                    [{planosDeAcaoQueryContext.DatabaseName}]..[Responsaveis] r with(nolock)
                    on i.[Id] = r.[ItemId]
                where
                    pa.[Id] = @planoDeAcaoId
                order by
					i.[Codigo], r.[NomeCompleto]";

            var obterPlanoDeAcaoQueryResultList = new List<ObterPlanoDeAcaoQueryResult>();

            await planosDeAcaoQueryContext
                .QueryAsync<ObterPlanoDeAcaoQueryResult, 
                ObterPlanoDeAcaoQueryResult.ObterPlanoDeAcaoQueryResultInnerItem, 
                ObterPlanoDeAcaoQueryResult.ObterPlanoDeAcaoQueryResultInnerResponsavel>(query, 
                    (planoDeAcao, item, responsavel) =>
                    {
                        var planoDeAcaoOutput = obterPlanoDeAcaoQueryResultList.FirstOrDefault(p => p.Id.Value == planoDeAcao.Id.Value);
                        if (planoDeAcaoOutput == null)
                        {
                            planoDeAcaoOutput = planoDeAcao;
                            planoDeAcaoOutput.Itens = new List<ObterPlanoDeAcaoQueryResult.ObterPlanoDeAcaoQueryResultInnerItem>();

                            obterPlanoDeAcaoQueryResultList.Add(planoDeAcaoOutput);
                        }

                        if (item != null)
                        {
                            var itemOutput = planoDeAcaoOutput.Itens.FirstOrDefault(i => i.ItemId.Value == item.ItemId.Value);
                            if (itemOutput == null)
                            {
                                itemOutput = item;
                                itemOutput.Responsaveis = new List<ObterPlanoDeAcaoQueryResult.ObterPlanoDeAcaoQueryResultInnerResponsavel>();

                                planoDeAcaoOutput.Itens.Add(item);
                            }

                            if (responsavel != null)
                                itemOutput.Responsaveis.Add(responsavel);
                        }

                        return planoDeAcaoOutput;
                    },
                    param: new
                    {
                        request.PlanoDeAcaoId
                    },
                    splitOn: "ItemId,ResponsavelId");

            return obterPlanoDeAcaoQueryResultList.FirstOrDefault();
        }
    }
}