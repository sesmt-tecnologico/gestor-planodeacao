using SESMTTech.Gestor.PlanosDeAcao.Domain.Exceptions;
using Furiza.Base.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate
{
    public class PlanoDeAcao : Entity, IAggregateRoot
    {
        public string Codigo { get; private set; }
        public int Ano { get; private set; }
        public int Numero { get; private set; }

        public IReadOnlyCollection<Item> Itens => _itens;
        private readonly List<Item> _itens;

        private PlanoDeAcao()
        {
            _itens = new List<Item>();
        }

        public PlanoDeAcao(int numero) : this()
        {
            if (numero <= 0)
                throw new NumeroDeIdentificacaoInvalidoException();

            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            Codigo = $"PDA-{DateTime.Now.Year}/{numero.ToString().PadLeft(4, '0')}";
            Ano = DateTime.Now.Year;
            Numero = numero;
        }

        public void ConcluirItens()
        {
            var dataRealizacao = DateTime.Now.Date;
            foreach (var item in _itens.Where(i => i.Status != StatusAgendamento.Realizado && i.Status != StatusAgendamento.Cancelado))
                item.Concluir(dataRealizacao);
        }

        public void AdicionarItem(string descricao, string acao, DateTime prazo, string nomeCompletoResponsavel, string emailResponsavel)
        {
            var numeroItem = _itens.Any()
                ? _itens.Max(i => i.Numero) + 1
                : 1;

            _itens.Add(new Item(Codigo, numeroItem, descricao, acao, prazo, nomeCompletoResponsavel, emailResponsavel));
        }

        public Item ObterItem(Guid itemId)
        {
            var item = _itens.FirstOrDefault(i => i.Id == itemId) ??
                throw new ItemDePlanoDeAcaoInvalidoException();

            return item;
        }

        public void DefinirNovoPrazoDoItem(Guid itemId, DateTime novoPrazo)
        {
            ObterItem(itemId).DefinirNovoPrazo(novoPrazo);
        }

        public void ConcluirItem(Guid itemId, DateTime dataRealizacao)
        {
            ObterItem(itemId).Concluir(dataRealizacao);
        }

        public void CancelarItem(Guid itemId)
        {
            ObterItem(itemId).Cancelar();
        }

        public void AdicionarResponsavelAoItem(Guid itemId, string nomeCompleto, string email)
        {
            ObterItem(itemId).AdicionarResponsavel(nomeCompleto, email);
        }

        public void RemoverResponsavelDoItem(Guid itemId, Guid responsavelId)
        {
            ObterItem(itemId).RemoverResponsavel(responsavelId);
        }
    }
}