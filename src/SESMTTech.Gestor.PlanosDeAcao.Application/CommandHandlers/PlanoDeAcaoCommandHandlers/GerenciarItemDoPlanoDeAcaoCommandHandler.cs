using Furiza.Audit.Abstractions;
using Furiza.Base.Core.Identity.Abstractions;
using MediatR;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Commands.PlanoDeAcaoCommands;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SESMTTech.Gestor.PlanosDeAcao.Application.CommandHandlers.PlanoDeAcaoCommandHandlers
{
    internal class GerenciarItemDoPlanoDeAcaoCommandHandler : IRequestHandler<AdicionarItemAoPlanoDeAcaoCommand>,
        IRequestHandler<AtualizarItemDoPlanoDeAcaoCommand>,
        IRequestHandler<AdicionarResponsavelAoItemDoPlanoDeAcaoCommand>,
        IRequestHandler<RemoverResponsavelDoItemDoPlanoDeAcaoCommand>,
        IRequestHandler<ConcluirItensDoPlanoDeAcaoCommand>,
        IRequestHandler<ConcluirItemDoPlanoDeAcaoCommand>,
        IRequestHandler<CancelarItemDoPlanoDeAcaoCommand>,
        IRequestHandler<RemoverItemDoPlanoDeAcaoCommand>
    {
        private readonly IUserPrincipalBuilder userPrincipalBuilder;
        private readonly IPlanoDeAcaoWriteOnlyRepository planoDeAcaoWriteOnlyRepository;
        private readonly IAuditTrailProvider auditTrailProvider;

        public GerenciarItemDoPlanoDeAcaoCommandHandler(IUserPrincipalBuilder userPrincipalBuilder,
            IPlanoDeAcaoWriteOnlyRepository planoDeAcaoWriteOnlyRepository,
            IAuditTrailProvider auditTrailProvider)
        {
            this.userPrincipalBuilder = userPrincipalBuilder ?? throw new ArgumentNullException(nameof(userPrincipalBuilder));
            this.planoDeAcaoWriteOnlyRepository = planoDeAcaoWriteOnlyRepository ?? throw new ArgumentNullException(nameof(planoDeAcaoWriteOnlyRepository));
            this.auditTrailProvider = auditTrailProvider ?? throw new ArgumentNullException(nameof(auditTrailProvider));
        }

        public async Task<Unit> Handle(AdicionarItemAoPlanoDeAcaoCommand request, CancellationToken cancellationToken)
        {
            request.PlanoDeAcao.AdicionarItem(request.Descricao, request.Acao, request.Prazo, request.NomeCompletoResponsavel, request.EmailResponsavel);

            return await ProcederComAAtualizacaoDoPlanoDeAcaoAsync(request.PlanoDeAcao);
        }

        public async Task<Unit> Handle(AtualizarItemDoPlanoDeAcaoCommand request, CancellationToken cancellationToken)
        {
            request.PlanoDeAcao.ObterItem(request.ItemId).Descricao = request.Descricao?.Trim();
            request.PlanoDeAcao.ObterItem(request.ItemId).Acao = request.Acao?.Trim();
            request.PlanoDeAcao.DefinirNovoPrazoDoItem(request.ItemId, request.Prazo);

            return await ProcederComAAtualizacaoDoPlanoDeAcaoAsync(request.PlanoDeAcao);
        }

        public async Task<Unit> Handle(AdicionarResponsavelAoItemDoPlanoDeAcaoCommand request, CancellationToken cancellationToken)
        {
            request.PlanoDeAcao.AdicionarResponsavelAoItem(request.ItemId, request.NomeCompleto, request.Email);

            return await ProcederComAAtualizacaoDoPlanoDeAcaoAsync(request.PlanoDeAcao);
        }

        public async Task<Unit> Handle(RemoverResponsavelDoItemDoPlanoDeAcaoCommand request, CancellationToken cancellationToken)
        {
            request.PlanoDeAcao.RemoverResponsavelDoItem(request.ItemId, request.ResponsavelId);

            return await ProcederComAAtualizacaoDoPlanoDeAcaoAsync(request.PlanoDeAcao);
        }

        public async Task<Unit> Handle(ConcluirItensDoPlanoDeAcaoCommand request, CancellationToken cancellationToken)
        {
            request.PlanoDeAcao.ConcluirItens();

            return await ProcederComAAtualizacaoDoPlanoDeAcaoAsync(request.PlanoDeAcao);
        }        

        public async Task<Unit> Handle(ConcluirItemDoPlanoDeAcaoCommand request, CancellationToken cancellationToken)
        {
            request.PlanoDeAcao.ConcluirItem(request.ItemId, request.DataRealizacao);

            return await ProcederComAAtualizacaoDoPlanoDeAcaoAsync(request.PlanoDeAcao);
        }

        public async Task<Unit> Handle(CancelarItemDoPlanoDeAcaoCommand request, CancellationToken cancellationToken)
        {
            request.PlanoDeAcao.CancelarItem(request.ItemId);

            return await ProcederComAAtualizacaoDoPlanoDeAcaoAsync(request.PlanoDeAcao);
        }

        public async Task<Unit> Handle(RemoverItemDoPlanoDeAcaoCommand request, CancellationToken cancellationToken)
        {
            request.PlanoDeAcao.RemoverItem(request.ItemId);

            return await ProcederComAAtualizacaoDoPlanoDeAcaoAsync(request.PlanoDeAcao);
        }

        #region [+] Privates
        private async Task<Unit> ProcederComAAtualizacaoDoPlanoDeAcaoAsync(PlanoDeAcao planoDeAcao)
        {
            planoDeAcaoWriteOnlyRepository.Update(planoDeAcao);

            await planoDeAcaoWriteOnlyRepository.UnitOfWork.SaveEntitiesAsync();

            await auditTrailProvider.AddTrailsAsync(AuditOperation.Update, userPrincipalBuilder.UserPrincipal.UserName, new AuditableObjects<PlanoDeAcao>(planoDeAcao.Id.ToString(), planoDeAcao));

            return Unit.Value;
        } 
        #endregion
    }
}