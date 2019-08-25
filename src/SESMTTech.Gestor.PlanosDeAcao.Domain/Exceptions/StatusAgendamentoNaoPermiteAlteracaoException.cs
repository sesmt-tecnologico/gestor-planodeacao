using Furiza.Base.Core.Exceptions;
using System;
using System.Runtime.Serialization;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Exceptions
{
    [Serializable]
    public class StatusAgendamentoNaoPermiteAlteracaoException : CoreException
    {
        public override string Key => "StatusAgendamentoNaoPermiteAlteracao";
        public override string Message => "O status de agendamento do item em questão não permite mais nenhuma alteração.";

        public StatusAgendamentoNaoPermiteAlteracaoException() : base()
        {
        }

        protected StatusAgendamentoNaoPermiteAlteracaoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}