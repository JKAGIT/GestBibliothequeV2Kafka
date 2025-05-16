using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Reservations
{
    public class EmprunterLivreReserveCommand : IRequest<bool>
    {
        public Guid IDReservation { get; set; }

        public EmprunterLivreReserveCommand(Guid idReservation)
        {
            IDReservation = idReservation;
        }
    }
}
