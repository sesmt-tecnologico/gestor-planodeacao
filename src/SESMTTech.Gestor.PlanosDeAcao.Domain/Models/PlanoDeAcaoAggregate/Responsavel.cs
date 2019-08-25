using SESMTTech.Gestor.PlanosDeAcao.Domain.Exceptions;
using Furiza.Base.Core.SeedWork;
using System;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate
{
    public class Responsavel : Entity
    {
        public string NomeCompleto { get; private set; }
        public string Email { get; private set; }

        private Responsavel() { }

        public Responsavel(string nomeCompleto, string email) : this()
        {
            if (string.IsNullOrWhiteSpace(nomeCompleto))
                throw new NomeNaoPodeSerNuloException();

            if (string.IsNullOrWhiteSpace(email))
                throw new EmailNaoPodeSerNuloException();

            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            NomeCompleto = nomeCompleto.Trim();
            Email = email.Trim();
        }
    }
}