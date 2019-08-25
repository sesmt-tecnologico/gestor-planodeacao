using Furiza.Base.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Exceptions
{
    [Serializable]
    public class ItemDePlanoDeAcaoSemResponsavelException : CoreException
    {
        public override string Key => "ItemDePlanoDeAcaoSemResponsavel";
        public override string Message => "O item de plano de ação especificado deve possuir, ao menos, 1 responsável.";

        public ItemDePlanoDeAcaoSemResponsavelException() : base()
        {
        }

        protected ItemDePlanoDeAcaoSemResponsavelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}