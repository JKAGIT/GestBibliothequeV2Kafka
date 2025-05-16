using GestionDesLivres.Application.Commands;
using GestionDesLivres.Application.Commands.Usagers;
using GestionDesLivres.Application.Queries;
using GestionDesLivres.Application.Queries.Usagers;
using GestionDesLivres.Domain;
using GestionDesLivres.Domain.Exceptions;
//using GestionDesUsagers.Application.Queries.Usagers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GestionDesLivres.API.Controllers
{
    [Route("api/usagers")]
    [ApiController]
    public class UsagerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("ajouter")]
        public async Task<IActionResult> AjouterUsager([FromBody] AjouterUsagerCommand command)
        {
            if (command == null)
                return BadRequest("Les données de l'usager sont manquantes.");

            try
            {
                var usagerId = await _mediator.Send(command);
                return CreatedAtAction(nameof(AjouterUsager), new { id = usagerId }, new { Id = usagerId });
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("modifier/{id}")]
        public async Task<IActionResult> MettreAJourUsager(Guid id, [FromBody] MettreAJourUsagerCommand command)
        {
            try
            {
                if (id != command.Id)
                    return BadRequest("L'ID fourni ne correspond pas à l'ID de l'usager.");

               var result = await _mediator.Send(command);
                return result ? Ok("Catégorie mise à jour avec succès.") : BadRequest("Échec de la mise à jour.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("supprimer/{id}")]
        public async Task<IActionResult> SupprimerUsager(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new SupprimerUsagerCommand(id));
                return result ? Ok("Catégorie supprimée avec succès.") : BadRequest("Échec de la suppression de la catégorie.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite: {ex.Message}");
            }
        }

        [HttpGet("usagerParId/{id}")]
        public async Task<IActionResult> ObtenirUsagerParId(Guid id)
        {
            var usager = await _mediator.Send(new ObtenirUsagerParIdQuery(id));
            return usager != null ? Ok(usager) : NotFound();
        }

        [HttpGet()]
        public async Task<IActionResult> ObtenirTousLesUsagers()
        {
            try
            {
                var query = new ObtenirTousUsagersQuery();
                var usagers = await _mediator.Send(query);
                return Ok(usagers);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
