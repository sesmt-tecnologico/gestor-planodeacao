using SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate;
using MediatR;
using System;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Commands.PlanoDeAcaoCommands
{
    public class AdicionarItemAoPlanoDeAcaoCommand : IRequest
    {
        public PlanoDeAcao PlanoDeAcao { get; set; }
        public DateTime Prazo { get; set; }
        public string NomeCompletoResponsavel { get; set; }
        public string EmailResponsavel { get; set; }
        public string Descricao { get; set; }
        public string Acao { get; set; }
    }
}