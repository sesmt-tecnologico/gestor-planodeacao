using Furiza.Base.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Exceptions
{
    [Serializable]
    public class CodigoNaoPodeSerNuloException : CoreException
    {
        public override string Key => "CodigoNaoPodeSerNulo";
        public override string Message => "O código do objeto não pode estar em branco.";

        public CodigoNaoPodeSerNuloException() : base()
        {
        }

        protected CodigoNaoPodeSerNuloException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}