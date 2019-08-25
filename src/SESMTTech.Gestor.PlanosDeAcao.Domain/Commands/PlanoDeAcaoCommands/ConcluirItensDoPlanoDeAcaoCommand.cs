using SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate;
using MediatR;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Commands.PlanoDeAcaoCommands
{
    public class ConcluirItensDoPlanoDeAcaoCommand : IRequest
    {
        public PlanoDeAcao PlanoDeAcao { get; set; }
    }
}