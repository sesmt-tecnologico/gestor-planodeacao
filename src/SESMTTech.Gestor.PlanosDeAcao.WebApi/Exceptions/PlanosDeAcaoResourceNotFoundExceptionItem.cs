using Furiza.AspNetCore.ExceptionHandling;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi.Exceptions
{
    public class PlanosDeAcaoResourceNotFoundExceptionItem : ResourceNotFoundExceptionItem
    {
        public static PlanosDeAcaoResourceNotFoundExceptionItem PlanoDeAcao => new PlanosDeAcaoResourceNotFoundExceptionItem("PlanoDeAcao", "Plano de Ação não encontrado.");

        private PlanosDeAcaoResourceNotFoundExceptionItem(string key, string message) : base(key, message)
        {
        }
    }
}