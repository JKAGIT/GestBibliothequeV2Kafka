using GestionDesLivres.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Reservations
{
    public class AjouterReservationCommand : IRequest<Guid>
    {
        public Guid IDUsager { get; set; }
        public Guid IDLivre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateRetourEstimee { get; set; }

        public AjouterReservationCommand(Guid idUsager, Guid idLivre, DateTime dateDebut, DateTime dateRetourEstimee)
        {
            IDUsager = idUsager;
            IDLivre = idLivre;
            DateDebut = dateDebut;
            DateRetourEstimee = dateRetourEstimee;
        }
    }

}



