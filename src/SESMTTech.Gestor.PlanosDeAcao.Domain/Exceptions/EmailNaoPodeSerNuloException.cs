using Furiza.Base.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Exceptions
{
    [Serializable]
    public class EmailNaoPodeSerNuloException : CoreException
    {
        public override string Key => "EmailNaoPodeSerNulo";
        public override string Message => "O email pessoal não pode estar em branco.";

        public EmailNaoPodeSerNuloException() : base()
        {
        }

        protected EmailNaoPodeSerNuloException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}