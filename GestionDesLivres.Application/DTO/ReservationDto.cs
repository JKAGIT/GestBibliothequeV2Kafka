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
    public class ReservationDto
    {
        public Guid ID { get; set; } 
        public Guid IDUsager { get; set; }
        public Guid IDLivre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateRetourEstimee { get; set; }
        public bool Annuler { get; set; }       
        public string NomCompletUsager { get; set; }
        public string TitreLivre { get; set; }
        public string LivreDisponible { get; set; } 
        public string StatutReservation { get; set; } 
        public bool EstEmprunte { get; set; }
    }
}
