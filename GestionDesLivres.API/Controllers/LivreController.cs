using GestionDesLivres.Application.Commands.Livres;
using GestionDesLivres.Application.Queries.Livres;
using GestionDesLivres.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionDesLivres.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LivreController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("ajouter")]
        public async Task<IActionResult> AjouterLivre([FromBody] AjouterLivreCommand command)
        {
            if (command == null)
                return BadRequest("Les données du livre sont requises.");

            try
            {
                var livreId = await _mediator.Send(command);
                return CreatedAtAction(nameof(AjouterLivre), new { id = livreId }, new { Id = livreId });
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

        // PUT api/<LivreController>/5       
        [HttpPut("modifier/{id}")]
        public async Task<IActionResult> MettreAJourLivre(Guid id, [FromBody] MettreAJourLivreCommand command)
        {
            try
            {
                if (command == null || id != command.Id)
                    return BadRequest("Les données du livre sont invalides.");

                var result = await _mediator.Send(command);
                return result ? Ok("Livre mis à jour avec succès.") : BadRequest("Échec de la mise à jour.");
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

        [HttpPatch("stock/{id}")]
        public async Task<IActionResult> MettreAJourStockLivre(Guid id, [FromBody] MettreAJourStockLivreCommand command)
        {
            if (command == null || id != command.Id)
                return BadRequest("Les données du livre sont invalides.");

            try
            {
                var resultat = await _mediator.Send(command);
                if (!resultat)
                    return NotFound($"Livre avec l'ID {id} non trouvé.");

                return NoContent();
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

        // DELETE api/<LivreController>/5
        [HttpDelete("supprimer/{id}")]
        public async Task<IActionResult> SupprimerLivre(Guid id)
        {
            var command = new SupprimerLivreCommand(id);
            try
            {
                var resultat = await _mediator.Send(command);
                if (!resultat)
                    return NotFound($"Livre avec l'ID {id} non trouvé.");

                return NoContent();
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

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenirLivreParId(Guid id)
        {
            try
            {
                var query = new ObtenirLivreParIdQuery(id);
                var livre = await _mediator.Send(query);
                return Ok(livre);
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

        [HttpGet("en-stock")]
        public async Task<IActionResult> ObtenirLivresEnStock()
        {
            try
            {
                var query = new ObtenirLivresEnStockQuery();
                var livresEnStock = await _mediator.Send(query);
                return Ok(livresEnStock);
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


        [HttpGet()]
        public async Task<IActionResult> ObtenirTousLesLivres()
        {
            try
            {
                var query = new ObtenirTousLivresQuery();
                var livres = await _mediator.Send(query);
                return Ok(livres);
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

        [HttpGet("categorie/{categorieId}")]
        public async Task<IActionResult> ObtenirLivresParCategorie(Guid categorieId)
        {
            try
            {
                var livres = await _mediator.Send(new ObtenirLivresParCategorieQuery(categorieId));
                if (livres == null || !livres.Any())
                    return NotFound($"Aucun livre trouvé pour la catégorie {categorieId}.");

                return Ok(livres);
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


        [HttpGet("disponibilite/{id}")]
        public async Task<IActionResult> VerifierDisponibiliteLivre(Guid id)
        {
            try
            {
                var livre = await _mediator.Send(new VerifierDisponibiliteLivreQuery(id));
                return Ok(livre);
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
