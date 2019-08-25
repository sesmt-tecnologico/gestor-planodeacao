using SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate;
using MediatR;
using System;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Commands.PlanoDeAcaoCommands
{
    public class AtualizarItemDoPlanoDeAcaoCommand : IRequest
    {
        public PlanoDeAcao PlanoDeAcao { get; set; }
        public Guid ItemId { get; set; }
        public string Descricao { get; set; }
        public string Acao { get; set; }
        public DateTime Prazo { get; set; }
    }
}