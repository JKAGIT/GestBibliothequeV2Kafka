using GestionDesLivres.Application.Commands.Categories;
using GestionDesLivres.Application.Queries.Categories;
using GestionDesLivres.Application.Queries.Livres;
using GestionDesLivres.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace GestionDesLivres.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategorieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("ajouter")]
        public async Task<IActionResult> AjouterCategorie([FromBody] AjouterCategorieCommand command)
        {
            if (command == null)
                return BadRequest("Les données de la catégorie sont manquantes.");

            try
            {
                Guid categorieId = await _mediator.Send(command);
                return CreatedAtAction(nameof(AjouterCategorie), new { id = categorieId }, categorieId);
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
        public async Task<IActionResult> MettreAJourCategorie(Guid id, [FromBody] MettreAJourCategorieCommand command)
        {
            if (id != command.Id)
                return BadRequest("L'ID de l'URL ne correspond pas à celui de la requête.");
            try
            {
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
        public async Task<IActionResult> SupprimerCategorie(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new SupprimerCategorieCommand(id));
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


        [HttpGet("categorieParId/{id}")]
        public async Task<IActionResult> ObtenirCategorieParId(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new ObtenirCategorieParIdQuery(id));
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

        [HttpGet("categorieParCode/{code}")]
        public async Task<IActionResult> ObtenirCategorieParCode(string code)
        {
            try
            {
                var result = await _mediator.Send(new ObtenirCategorieParCodeQuery(code));
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


        [HttpGet()]
        public async Task<IActionResult> ObtenirToutesCategories()
        {
            try
            {
                var query = new ObtenirTousCategoriesQuery();
                var categories = await _mediator.Send(query);
                return Ok(categories);
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
