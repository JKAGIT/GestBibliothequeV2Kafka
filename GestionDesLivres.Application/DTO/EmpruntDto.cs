using GestionDesLivres.Domain.Common;
using GestionDesLivres.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.DTO
{
    public class EmpruntDto
    {
        public Guid ID { get; set; }

        public Guid? IDReservation { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateRetourPrevue { get; set; }
        public bool EstRendu { get; set; }
        public DateTime? DateRetour { get; set; } //date retour effective
        public Guid IDUsager { get; set; }
        public Guid IDLivre { get; set; }
        public string NomCompletUsager { get; set; }
        public string TitreLivre { get; set; }
        public bool EnRetard => !EstRendu && DateRetourPrevue < DateTime.Now;
    }
}

