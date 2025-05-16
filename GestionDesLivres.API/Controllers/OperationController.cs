using GestionDesLivres.Application.Queries.Categories;
using GestionDesLivres.Application.Queries.Livres;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Application.Commands.Livres;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionDesLivres.API.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OperationController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("verifdisponiblite/{id}")]
        public async Task<IActionResult> VerifierDisponibilite(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new VerifierDisponibiliteLivreQuery(id));
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


        [HttpGet("disponible")]
        public async Task<IActionResult> ObtenirTousLivresDisponible()
        {
            try
            {
                var query = new ObtenirTousLivresDisponibleQuery();
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



        [HttpPut("stock/{id}")]
        public async Task<IActionResult> MettreAJourStock([FromBody] MettreAJourStockLivreCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                if (result)
                    return Ok("Stock mis à jour avec succès.");
                else
                    return BadRequest("Échec de la mise à jour du stock.");
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
