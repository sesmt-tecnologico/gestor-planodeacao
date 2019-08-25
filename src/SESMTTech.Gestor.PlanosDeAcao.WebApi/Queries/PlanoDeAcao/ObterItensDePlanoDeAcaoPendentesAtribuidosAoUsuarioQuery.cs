using MediatR;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi.Queries.PlanoDeAcao
{
    public class ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQuery : IRequest<ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQueryResult>
    {
        public string Email { get; set; }
    }
}