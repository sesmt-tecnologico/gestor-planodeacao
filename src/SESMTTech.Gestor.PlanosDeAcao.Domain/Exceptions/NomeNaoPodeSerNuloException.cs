using Furiza.Base.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Exceptions
{
    [Serializable]
    public class NomeNaoPodeSerNuloException : CoreException
    {
        public override string Key => "NomeNaoPodeSerNulo";
        public override string Message => "O nome do objeto não pode estar em branco.";

        public NomeNaoPodeSerNuloException() : base()
        {
        }

        protected NomeNaoPodeSerNuloException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}