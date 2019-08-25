using SESMTTech.Gestor.PlanosDeAcao.Domain.Exceptions;
using Furiza.Base.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate
{
    public class Item : Entity
    {
        public int Numero { get; private set; }
        public string Codigo { get; private set; }
        public string Descricao { get; set; }
        public string Acao { get; set; }
        public DateTime Prazo { get; private set; }
        public StatusAgendamento Status { get; private set; }
        public DateTime? DataRealizacao { get; private set; }

        public IReadOnlyCollection<Responsavel> Responsaveis => _responsaveis;
        private readonly List<Responsavel> _responsaveis;

        private Item()
        {
            _responsaveis = new List<Responsavel>();
        }

        public Item(string codigoPlanoDeAcao, int numero, string descricao, string acao, DateTime prazo, string nomeCompletoResponsavel, string emailResponsavel) : this()
        {
            if (string.IsNullOrWhiteSpace(codigoPlanoDeAcao))
                throw new CodigoNaoPodeSerNuloException();

            if (numero <= 0)
                throw new NumeroDeIdentificacaoInvalidoException();

            if (prazo < DateTime.Now)
                throw new DataNaoPodeSerNoPassadoException();

            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
            Numero = numero;
            Codigo = $"{codigoPlanoDeAcao}-ITEM-{numero.ToString().PadLeft(2, '0')}";
            Descricao = descricao?.Trim();
            Acao = acao?.Trim();
            Prazo = prazo;
            Status = StatusAgendamento.Programado;

            AdicionarResponsavel(nomeCompletoResponsavel, emailResponsavel);
        }

        public void DefinirNovoPrazo(DateTime novoPrazo)
        {
            ValidarSeStatusPermiteAlteracao();

            if (novoPrazo < DateTime.Now)
                throw new DataNaoPodeSerNoPassadoException();

            if (novoPrazo < Prazo)
                Status = StatusAgendamento.Adiantado;
            else
                Status = StatusAgendamento.Adiado;

            Prazo = novoPrazo;
        }

        public void Concluir(DateTime dataRealizacao)
        {
            ValidarSeStatusPermiteAlteracao();

            DataRealizacao = dataRealizacao;
            Status = StatusAgendamento.Realizado;
        }

        public void Cancelar()
        {
            ValidarSeStatusPermiteAlteracao();

            Status = StatusAgendamento.Cancelado;
        }

        public void AdicionarResponsavel(string nomeCompleto, string email)
        {
            ValidarSeStatusPermiteAlteracao();

            if (_responsaveis.Any(r => r.Email == email))
                throw new ResponsavelDoItemDoPlanoDeAcaoJaExistenteException();

            _responsaveis.Add(new Responsavel(nomeCompleto, email));
        }

        public void RemoverResponsavel(Guid responsavelId)
        {
            ValidarSeStatusPermiteAlteracao();

            if (_responsaveis.Count <= 1)
                throw new ItemDePlanoDeAcaoSemResponsavelException();

            var responsavel = _responsaveis.FirstOrDefault(m => m.Id == responsavelId);
            if (responsavel != null)
                _responsaveis.Remove(responsavel);
        }

        #region [+] Privates
        private void ValidarSeStatusPermiteAlteracao()
        {
            if (Status == StatusAgendamento.Realizado || Status == StatusAgendamento.Cancelado)
                throw new StatusAgendamentoNaoPermiteAlteracaoException();
        }
        #endregion
    }
}