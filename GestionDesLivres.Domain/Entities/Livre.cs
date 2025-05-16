
using GestionDesLivres.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace GestionDesLivres.Domain.Entities
{
    public class Livre : BaseEntity, IAggregateRoot
    {
        [Required(ErrorMessage = "Le titre est obligatoire.")]
        public string Titre { get; set; } = string.Empty;
        [Required(ErrorMessage = "L'auteur est obligatoire.")]
        public string Auteur { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le Nombre est obligatoire.")]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez saisir un nombre positif.")]
        public int Stock { get; set; }
        public Guid IDCategorie { get; set; }


        public Categorie? Categorie { get; set; }
        public ICollection<Emprunt> Emprunts { get; set; } = new List<Emprunt>();
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        public bool EstValide()
        {
            return !string.IsNullOrEmpty(Titre) &&
                   !string.IsNullOrEmpty(Auteur) &&
                   IDCategorie != Guid.Empty;
        }
    }
}
