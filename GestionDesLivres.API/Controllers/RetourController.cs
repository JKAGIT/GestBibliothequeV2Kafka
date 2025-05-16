using GestionDesLivres.Application.Commands.Retours;
using GestionDesLivres.Application.Retours.Commands;
using GestionDesLivres.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GestionDesLivres.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetourController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RetourController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("ajouter")]
        public async Task<IActionResult> AjouterRetour([FromBody] AjouterRetourCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var idRetour = await _mediator.Send(command);
                return CreatedAtAction(nameof(AjouterRetour), new { id = idRetour }, new { id = idRetour });
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
