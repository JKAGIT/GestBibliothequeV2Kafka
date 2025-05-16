using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Reservations
{
    public class AnnulerReservationCommand : IRequest<bool>
    {
        public Guid IDReservation { get; set; }

        public AnnulerReservationCommand(Guid idReservation)
        {
            IDReservation = idReservation;
        }
    }

}
