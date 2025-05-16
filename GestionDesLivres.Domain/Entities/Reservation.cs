
using GestionDesLivres.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace GestionDesLivres.Domain.Entities
{
    public class Reservation : BaseEntity, IAggregateRoot
    {
        [Required]
        public DateTime DateDebut { get; set; }

        [Required]
        public DateTime DateRetourEstimee { get; set; }

        public bool Annuler { get; set; }

        public Guid IDUsager { get; set; }
        public Usager? Usager { get; set; }

        public Guid IDLivre { get; set; }
        public Livre? Livre { get; set; }
        public virtual Emprunt? Emprunt { get; set; }
    }
}
