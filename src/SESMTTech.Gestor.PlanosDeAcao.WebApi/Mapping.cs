using AutoMapper;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Commands.PlanoDeAcaoCommands;
using SESMTTech.Gestor.PlanosDeAcao.WebApi.Dtos.v1.PlanosDeAcao;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ItensPlanosDeAcaoPost, AdicionarItemAoPlanoDeAcaoCommand>();
            CreateMap<ConcluirItensPlanosDeAcaoPost, ConcluirItemDoPlanoDeAcaoCommand>();
            CreateMap<ItensPlanosDeAcaoPut, AtualizarItemDoPlanoDeAcaoCommand>();
            CreateMap<ResponsaveisItensPlanosDeAcaoPost, AdicionarResponsavelAoItemDoPlanoDeAcaoCommand>();
        }
    }
}