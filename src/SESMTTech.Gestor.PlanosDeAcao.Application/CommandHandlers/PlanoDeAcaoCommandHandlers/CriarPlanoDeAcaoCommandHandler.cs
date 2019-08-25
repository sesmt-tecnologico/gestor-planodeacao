using Furiza.Audit.Abstractions;
using Furiza.Base.Core.Identity.Abstractions;
using MediatR;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Commands;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Commands.PlanoDeAcaoCommands;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SESMTTech.Gestor.PlanosDeAcao.Application.CommandHandlers.PlanoDeAcaoCommandHandlers
{
    internal class CriarPlanoDeAcaoCommandHandler : IRequestHandler<CriarPlanoDeAcaoCommand, CriarCommandResult>
    {
        private readonly IUserPrincipalBuilder userPrincipalBuilder;
        private readonly IPlanoDeAcaoReadOnlyRepository planoDeAcaoReadOnlyRepository;
        private readonly IPlanoDeAcaoWriteOnlyRepository planoDeAcaoWriteOnlyRepository;
        private readonly IAuditTrailProvider auditTrailProvider;

        public CriarPlanoDeAcaoCommandHandler(IUserPrincipalBuilder userPrincipalBuilder,
            IPlanoDeAcaoReadOnlyRepository planoDeAcaoReadOnlyRepository,
            IPlanoDeAcaoWriteOnlyRepository planoDeAcaoWriteOnlyRepository,
            IAuditTrailProvider auditTrailProvider)
        {
            this.userPrincipalBuilder = userPrincipalBuilder ?? throw new ArgumentNullException(nameof(userPrincipalBuilder));
            this.planoDeAcaoReadOnlyRepository = planoDeAcaoReadOnlyRepository ?? throw new ArgumentNullException(nameof(planoDeAcaoReadOnlyRepository));
            this.planoDeAcaoWriteOnlyRepository = planoDeAcaoWriteOnlyRepository ?? throw new ArgumentNullException(nameof(planoDeAcaoWriteOnlyRepository));
            this.auditTrailProvider = auditTrailProvider ?? throw new System.ArgumentNullException(nameof(auditTrailProvider));
        }

        public async Task<CriarCommandResult> Handle(CriarPlanoDeAcaoCommand request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var numeroProximoPlanoDeAcao = await planoDeAcaoReadOnlyRepository.GetNumeroDoUltimoPlanoDeAcaoAsync(DateTime.Now.Year) + 1;
            var planoDeAcao = new PlanoDeAcao(numeroProximoPlanoDeAcao)
            {
                CreationUser = userPrincipalBuilder.UserPrincipal.UserName
            };

            planoDeAcaoWriteOnlyRepository.Insert(planoDeAcao);

            await planoDeAcaoWriteOnlyRepository.UnitOfWork.SaveEntitiesAsync();

            await auditTrailProvider.AddTrailsAsync(AuditOperation.Create, userPrincipalBuilder.UserPrincipal.UserName, new AuditableObjects<PlanoDeAcao>(planoDeAcao.Id.ToString(), planoDeAcao));

            return new CriarCommandResult(planoDeAcao.Id, planoDeAcao.Codigo);
        }
    }
}