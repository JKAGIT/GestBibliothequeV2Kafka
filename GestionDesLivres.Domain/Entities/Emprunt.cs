using GestionDesLivres.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionDesLivres.Domain.Entities
{
    public class Emprunt : BaseEntity, IAggregateRoot
    {
        public Guid? IDReservation { get; set; }

        [Required(ErrorMessage = "La date de début est obligatoire.")]
        public DateTime DateDebut { get; set; }

        [Required(ErrorMessage = "La date de retour prévue est obligatoire.")]
        public DateTime DateRetourPrevue { get; set; }
        public bool EstRendu { get; set; }
        [ForeignKey("Categorie")]
        public Guid IDUsager { get; set; }
        public Usager? Usager { get; set; }

        [ForeignKey("Categorie")]
        public Guid IDLivre { get; set; }
        public Livre? Livre { get; set; }
        public virtual Retour? Retour { get; set; }
        public virtual Reservation? Reservation { get; set; }

    }
}
