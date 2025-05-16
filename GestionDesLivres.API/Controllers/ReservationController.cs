using GestionDesLivres.Application.Commands.Reservations;
using GestionDesLivres.Application.Queries.Categories;
using GestionDesLivres.Application.Queries.Emprunts;
using GestionDesLivres.Application.Queries.Reservations;
using GestionDesLivres.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestionDesLivres.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("ajouter")]
        public async Task<IActionResult> AjouterReservation([FromBody] AjouterReservationCommand command)
        {
            try
            {
                var reservationId = await _mediator.Send(command);
                return CreatedAtAction(nameof(AjouterReservation), new { id = reservationId }, new { Id = reservationId });
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
        public async Task<IActionResult> MettreAJourReservation(Guid id, [FromBody] MettreAJourReservationCommand command)
        {
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

        [HttpPut("annuler/{id}")]
        public async Task<IActionResult> AnnulerReservation(Guid id)
        {
            try { 
            await _mediator.Send(new AnnulerReservationCommand(id));
                return Ok("Annulation effectuée avec succès.");
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

        /// <summary>
        /// Emprunter un livre réservé
        /// </summary>
        [HttpPost("emprunter/{id}")]
        public async Task<IActionResult> EmprunterLivreReserve(Guid id)
        {
            try
            {
                await _mediator.Send(new EmprunterLivreReserveCommand(id));
                return Ok("Livre emprunté avec succès.");
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


        [HttpGet]
        public async Task<IActionResult> ObtenirToutesReservations()
        {
            try
            {
                var reservations = await _mediator.Send(new ObtenirToutesReservationsQuery());
                return Ok(reservations);
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

        [HttpGet("reservationParId/{id}")]
        public async Task<IActionResult> ObtenirReservationParId(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new ObtenirReservationsParIdQuery(id));
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



    }
}


