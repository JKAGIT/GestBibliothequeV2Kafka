using GestionDesLivres.Application.Commands.Emprunts;
using GestionDesLivres.Application.Queries.Categories;
using GestionDesLivres.Application.Queries.Emprunts;
using GestionDesLivres.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestionDesLivres.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpruntsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmpruntsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/emprunts
        [HttpGet]
        public async Task<IActionResult> ObtenirEmprunts()
        {
            try
            {
                var result = await _mediator.Send(new ObtenirTousLesEmpruntsQuery());
                return Ok(result);
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

        // GET: api/emprunts/usager/{idUsager}
        [HttpGet("usager/{idUsager}")]
        public async Task<IActionResult> GetByUsager(Guid idUsager)
        {
            try
            {
                var result = await _mediator.Send(new ObtenirEmpruntsParUsagerQuery(idUsager));
                return Ok(result);
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
        
        [HttpGet("empruntParId/{id}")]
        public async Task<IActionResult> ObtenirempruntParId(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new ObtenirEmpruntsParIdQuery(id));
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite: {ex.Message}");
            }
        }
        // POST: api/emprunts
        [HttpPost("Ajouter")]
        public async Task<IActionResult> AjouterEmprunt([FromBody] AjouterEmpruntCommand command)
        {
            if (command == null)
                return BadRequest("Les données de l'emprunt sont manquantes.");

            try
            {
                Guid empruntId = await _mediator.Send(command);                
                return CreatedAtAction(nameof(AjouterEmprunt), new { id = empruntId }, empruntId);
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

        // PUT: api/emprunts/{id}
        [HttpPut("modifier/{id}")]
        public async Task<IActionResult> ModifierEmprunt(Guid id, [FromBody] ModifierEmpruntCommand command)
        {
            try { 

            var result = await _mediator.Send(command);
            //return Ok(result);
                return result ? Ok("Emprunt mise à jour avec succès.") : BadRequest("Échec de la mise à jour.");
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

        // DELETE: api/emprunts/{id}
        [HttpDelete("supprimer/{id}")]
        public async Task<IActionResult> Supprimer(Guid id)
        {
            try { 
            var result = await _mediator.Send(new SupprimerEmpruntCommand(id));
                //return NoContent();
                return result ? Ok("Emprunt supprimé avec succès.") : BadRequest("Échec de la suppression.");
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
    }
}
