using Furiza.Base.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Exceptions
{
    [Serializable]
    public class NumeroDeIdentificacaoInvalidoException : CoreException
    {
        public override string Key => "NumeroDeIdentificacaoInvalido";
        public override string Message => "O número de identificação informado é inválido";

        public NumeroDeIdentificacaoInvalidoException() : base()
        {
        }

        protected NumeroDeIdentificacaoInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}