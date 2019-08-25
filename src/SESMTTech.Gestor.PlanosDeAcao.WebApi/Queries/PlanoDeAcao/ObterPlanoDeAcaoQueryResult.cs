using SESMTTech.Gestor.PlanosDeAcao.Domain.Models;
using System;
using System.Collections.Generic;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi.Queries.PlanoDeAcao
{
    public class ObterPlanoDeAcaoQueryResult
    {
        public Guid? Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }
        public string Codigo { get; set; }
        public string Ano { get; set; }
        public string Numero { get; set; }
        public ICollection<ObterPlanoDeAcaoQueryResultInnerItem> Itens { get; set; }

        public class ObterPlanoDeAcaoQueryResultInnerItem
        {
            public Guid? ItemId { get; set; }
            public string Codigo { get; set; }
            public string Descricao { get; set; }
            public string Acao { get; set; }
            public DateTime? Prazo { get; set; }
            public StatusAgendamento? Status { get; set; }
            public DateTime? DataRealizacao { get; set; }
            public ICollection<ObterPlanoDeAcaoQueryResultInnerResponsavel> Responsaveis { get; set; }
        }

        public class ObterPlanoDeAcaoQueryResultInnerResponsavel
        {
            public Guid? ResponsavelId { get; set; }
            public string NomeCompleto { get; set; }
            public string Email { get; set; }
        }
    }
}