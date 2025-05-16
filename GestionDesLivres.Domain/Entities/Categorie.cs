
using GestionDesLivres.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace GestionDesLivres.Domain.Entities
{
    public class Categorie:BaseEntity,IAggregateRoot
    {
        [Required(ErrorMessage = "Le Code est obligatoire.")]
        [StringLength(4, ErrorMessage = "Le Code doit être exactement de 4 caractères.")]
        [RegularExpression(@"^[a-zA-Z0-9]{4}$", ErrorMessage = "Le Code doit contenir uniquement 4 caractères alphanumériques.")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le Libellé est obligatoire.")]
        [StringLength(80, ErrorMessage = "Le Libellé ne doit pas dépasser 100 caractères.")]
        public string Libelle { get; set; } = string.Empty;
        public ICollection<Livre> Livres { get; set; } = new List<Livre>();

        public bool EstValide()
        {
            return !string.IsNullOrEmpty(Code) &&
                   !string.IsNullOrEmpty(Libelle);
        }
    }
}
