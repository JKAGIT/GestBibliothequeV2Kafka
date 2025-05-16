using GestionDesLivres.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Reservations
{
    public class ObtenirToutesReservationsQuery : IRequest<IEnumerable<ReservationDto>>
    { 
    }

}
