using Furiza.Base.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Exceptions
{
    [Serializable]
    public class PlanoDeAcaoInvalidoException : CoreException
    {
        public override string Key => "PlanoDeAcaoInvalido";
        public override string Message => "O plano de ação especificado é inválido ou não existe.";

        public PlanoDeAcaoInvalidoException() : base()
        {
        }

        protected PlanoDeAcaoInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}