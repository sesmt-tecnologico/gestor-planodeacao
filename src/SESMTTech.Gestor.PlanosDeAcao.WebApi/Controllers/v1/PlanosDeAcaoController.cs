using AutoMapper;
using Furiza.AspNetCore.ExceptionHandling;
using Furiza.AspNetCore.WebApi.Configuration;
using Furiza.Base.Core.Exceptions.Serialization;
using Furiza.Base.Core.Identity.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Commands;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Commands.PlanoDeAcaoCommands;
using SESMTTech.Gestor.PlanosDeAcao.Domain.Models.PlanoDeAcaoAggregate;
using SESMTTech.Gestor.PlanosDeAcao.WebApi.Dtos.v1.PlanosDeAcao;
using SESMTTech.Gestor.PlanosDeAcao.WebApi.Exceptions;
using SESMTTech.Gestor.PlanosDeAcao.WebApi.Queries.PlanoDeAcao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SESMTTech.Gestor.PlanosDeAcao.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PlanosDeAcaoController : RootController
    {
        private readonly IPlanoDeAcaoReadOnlyRepository planoDeAcaoReadOnlyRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IUserPrincipalBuilder userPrincipalBuilder;

        public PlanosDeAcaoController(IPlanoDeAcaoReadOnlyRepository planoDeAcaoReadOnlyRepository,
            IMediator mediator,
            IMapper mapper,
            IUserPrincipalBuilder userPrincipalBuilder)
        {
            this.planoDeAcaoReadOnlyRepository = planoDeAcaoReadOnlyRepository ?? throw new ArgumentNullException(nameof(planoDeAcaoReadOnlyRepository));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.userPrincipalBuilder = userPrincipalBuilder ?? throw new ArgumentNullException(nameof(userPrincipalBuilder));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ObterPlanoDeAcaoQueryResult), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(BadRequestError), 404)]
        [ProducesResponseType(typeof(InternalServerError), 500)]
        public async Task<IActionResult> GetAsync([FromRoute]Guid id)
        {
            var query = new ObterPlanoDeAcaoQuery()
            {
                PlanoDeAcaoId = id
            };
            var queryResult = await mediator.Send(query);
            if (queryResult == null)
                throw new ResourceNotFoundException(new ResourceNotFoundExceptionItem[] { PlanosDeAcaoResourceNotFoundExceptionItem.PlanoDeAcao });

            return Ok(queryResult);
        }

        [Authorize(Policy = FurizaPolicies.RequireEditorRights)]
        [HttpPost]
        [ProducesResponseType(typeof(CriarCommandResult), 200)]
        [ProducesResponseType(typeof(BadRequestError), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(InternalServerError), 500)]
        public async Task<IActionResult> PostAsync()
        {
            var command = new CriarPlanoDeAcaoCommand();
            var commandResult = await mediator.Send(command);

            return Ok(commandResult);
        }

        [Authorize(Policy = FurizaPolicies.RequireEditorRights)]
        [HttpPost("{id}/Itens")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(BadRequestError), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(BadRequestError), 404)]
        [ProducesResponseType(typeof(BadRequestError), 406)]
        [ProducesResponseType(typeof(InternalServerError), 500)]
        public async Task<IActionResult> ItensPostAsync(Guid id,
            [FromBody]ItensPlanosDeAcaoPost model)
        {
            var planoDeAcao = await ObterPlanoDeAcaoAsync(id);

            var command = mapper.Map<ItensPlanosDeAcaoPost, AdicionarItemAoPlanoDeAcaoCommand>(model, opts =>
                opts.AfterMap((s, d) =>
                {
                    d.PlanoDeAcao = planoDeAcao;
                }));

            await mediator.Send(command);

            return Ok();
        }

        [Authorize(Policy = FurizaPolicies.RequireEditorRights)]
        [HttpPost("{id}/Itens/Concluir")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(BadRequestError), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(BadRequestError), 404)]
        [ProducesResponseType(typeof(InternalServerError), 500)]
        public async Task<IActionResult> ConcluirTodosItensPostAsync(Guid id)
        {
            var planoDeAcao = await ObterPlanoDeAcaoAsync(id);

            var command = new ConcluirItensDoPlanoDeAcaoCommand()
            {
                PlanoDeAcao = planoDeAcao
            };

            await mediator.Send(command);

            return Ok();
        }

        [Authorize(Policy = FurizaPolicies.RequireEditorRights)]
        [HttpPost("{id}/Itens/{itemId}/Concluir")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(BadRequestError), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(BadRequestError), 404)]
        [ProducesResponseType(typeof(BadRequestError), 406)]
        [ProducesResponseType(typeof(InternalServerError), 500)]
        public async Task<IActionResult> ConcluirItensPostAsync(Guid id,
            Guid itemId,
            [FromBody]ConcluirItensPlanosDeAcaoPost model)
        {
            var planoDeAcao = await ObterPlanoDeAcaoAsync(id);

            var command = mapper.Map<ConcluirItensPlanosDeAcaoPost, ConcluirItemDoPlanoDeAcaoCommand>(model, opts =>
                opts.AfterMap((s, d) =>
                {
                    d.PlanoDeAcao = planoDeAcao;
                    d.ItemId = itemId;
                }));

            await mediator.Send(command);

            return Ok();
        }

        [Authorize(Policy = FurizaPolicies.RequireEditorRights)]
        [HttpPost("{id}/Itens/{itemId}/Cancelar")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(BadRequestError), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(BadRequestError), 404)]
        [ProducesResponseType(typeof(InternalServerError), 500)]
        public async Task<IActionResult> CancelarItensPostAsync(Guid id,
            Guid itemId)
        {
            var planoDeAcao = await ObterPlanoDeAcaoAsync(id);

            var command = new CancelarItemDoPlanoDeAcaoCommand()
            {
                PlanoDeAcao = planoDeAcao,
                ItemId = itemId
            };

            await mediator.Send(command);

            return Ok();
        }

        [Authorize(Policy = FurizaPolicies.RequireEditorRights)]
        [HttpPut("{id}/Itens/{itemId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(BadRequestError), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(BadRequestError), 404)]
        [ProducesResponseType(typeof(BadRequestError), 406)]
        [ProducesResponseType(typeof(InternalServerError), 500)]
        public async Task<IActionResult> ItensPutAsync(Guid id,
            Guid itemId,
            [FromBody]ItensPlanosDeAcaoPut model)
        {
            var planoDeAcao = await ObterPlanoDeAcaoAsync(id);

            var command = mapper.Map<ItensPlanosDeAcaoPut, AtualizarItemDoPlanoDeAcaoCommand>(model, opts =>
                opts.AfterMap((s, d) =>
                {
                    d.PlanoDeAcao = planoDeAcao;
                    d.ItemId = itemId;
                }));

            await mediator.Send(command);

            return Ok();
        }

        [Authorize(Policy = FurizaPolicies.RequireEditorRights)]
        [HttpPost("{id}/Itens/{itemId}/Responsaveis")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(BadRequestError), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(BadRequestError), 404)]
        [ProducesResponseType(typeof(BadRequestError), 406)]
        [ProducesResponseType(typeof(InternalServerError), 500)]
        public async Task<IActionResult> ResponsaveisItensPostAsync(Guid id,
            Guid itemId,
            [FromBody]ResponsaveisItensPlanosDeAcaoPost model)
        {
            var planoDeAcao = await ObterPlanoDeAcaoAsync(id);

            var command = mapper.Map<ResponsaveisItensPlanosDeAcaoPost, AdicionarResponsavelAoItemDoPlanoDeAcaoCommand>(model, opts =>
                opts.AfterMap((s, d) =>
                {
                    d.PlanoDeAcao = planoDeAcao;
                    d.ItemId = itemId;
                }));

            await mediator.Send(command);

            return Ok();
        }

        [Authorize(Policy = FurizaPolicies.RequireEditorRights)]
        [HttpDelete("{id}/Itens/{itemId}/Responsaveis/{responsavelId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(BadRequestError), 400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(typeof(BadRequestError), 404)]
        [ProducesResponseType(typeof(InternalServerError), 500)]
        public async Task<IActionResult> ResponsaveisItensDeleteAsync(Guid id,
            Guid itemId,
            Guid responsavelId)
        {
            var planoDeAcao = await ObterPlanoDeAcaoAsync(id);

            var command = new RemoverResponsavelDoItemDoPlanoDeAcaoCommand()
            {
                PlanoDeAcao = planoDeAcao,
                ItemId = itemId,
                ResponsavelId = responsavelId
            };

            await mediator.Send(command);

            return Ok();
        }

        [HttpGet("Itens/PendentesAtribuidosAoUsuarioLogado")]
        [ProducesResponseType(typeof(ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQueryResult), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(InternalServerError), 500)]
        public async Task<IActionResult> ItensAtribuidosAoUsuarioLogadoGetAsync()
        {
            var query = new ObterItensDePlanoDeAcaoPendentesAtribuidosAoUsuarioQuery()
            {
                Email = userPrincipalBuilder.UserPrincipal.Email
            };
            var queryResult = await mediator.Send(query);

            return Ok(queryResult);
        }

        #region [+] Pvts
        private async Task<PlanoDeAcao> ObterPlanoDeAcaoAsync(Guid id)
        {
            var errors = new List<PlanosDeAcaoResourceNotFoundExceptionItem>();

            var planoDeAcao = await planoDeAcaoReadOnlyRepository.GetByIdAsync(id);
            if (planoDeAcao == null)
                errors.Add(PlanosDeAcaoResourceNotFoundExceptionItem.PlanoDeAcao);

            if (errors.Any())
                throw new ResourceNotFoundException(errors);

            return planoDeAcao;
        }
        #endregion
    }
}