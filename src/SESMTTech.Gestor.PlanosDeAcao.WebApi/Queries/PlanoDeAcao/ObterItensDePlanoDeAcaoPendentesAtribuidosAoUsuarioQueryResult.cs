using SESMTTech.Gestor.PlanosDeAcao.Domain.Models;
using System;
using System.Collections.Generic;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi.Queries.PlanoDeAcao
{
    public class ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQueryResult
    {
        public IEnumerable<ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQueryResultInnerItem> Itens { get; set; }

        public class ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQueryResultInnerItem
        {
            public Guid? Id { get; set; }
            public string Codigo { get; set; }
            public string Descricao { get; set; }
            public string Acao { get; set; }
            public DateTime? Prazo { get; set; }
            public StatusAgendamento? Status { get; set; }
            public Guid? PlanoDeAcaoId { get; set; }
        }
    }
}