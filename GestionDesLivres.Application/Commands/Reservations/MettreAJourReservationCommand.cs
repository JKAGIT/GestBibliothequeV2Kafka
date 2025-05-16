using GestionDesLivres.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Reservations
{
    public class MettreAJourReservationCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid IDUsager { get; set; }
        public Guid IDLivre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateRetourEstimee { get; set; }
        public bool Annuler { get; set; }

        public MettreAJourReservationCommand(Guid id, Guid idusager, Guid idLivre,DateTime dateDebut, DateTime dateRetourEstimee, bool annuler)
        {
            Id= id;
            IDUsager = idusager;
            IDLivre = idLivre;
            DateDebut = dateDebut;
            DateRetourEstimee = dateRetourEstimee;
            Annuler = annuler;
        }
    }

}
