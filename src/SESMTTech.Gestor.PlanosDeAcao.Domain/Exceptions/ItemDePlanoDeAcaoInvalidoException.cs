using Furiza.Base.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Exceptions
{
    [Serializable]
    public class ItemDePlanoDeAcaoInvalidoException : CoreException
    {
        public override string Key => "ItemDePlanoDeAcaoInvalido";
        public override string Message => "O item de plano de ação especificado é inválido ou não existe.";

        public ItemDePlanoDeAcaoInvalidoException() : base()
        {
        }

        protected ItemDePlanoDeAcaoInvalidoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}