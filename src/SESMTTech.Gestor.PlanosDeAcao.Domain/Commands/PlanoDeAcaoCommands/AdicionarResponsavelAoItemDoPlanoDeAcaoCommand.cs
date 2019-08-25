using SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate;
using MediatR;
using System;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Commands.PlanoDeAcaoCommands
{
    public class AdicionarResponsavelAoItemDoPlanoDeAcaoCommand : IRequest
    {
        public PlanoDeAcao PlanoDeAcao { get; set; }
        public Guid ItemId { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
    }
}