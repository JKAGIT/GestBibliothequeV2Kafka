using GestionDesLivres.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Emprunts
{
    public class ObtenirReservationsParIdQuery : IRequest<ReservationDto>
    {
        public Guid Id { get; set; }

        public ObtenirReservationsParIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
