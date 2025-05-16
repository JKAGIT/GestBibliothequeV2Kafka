using System.ComponentModel.DataAnnotations;

namespace GestBibliotheque.Blazor.Models
{
    public class LivreDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Le titre est obligatoire.")]
        public string Titre { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'auteur est obligatoire.")]
        public string Auteur { get; set; } = string.Empty;
        [Required(ErrorMessage = "La catégorie est obligatoire.")]
        public Guid CategorieId { get; set; }
        public string Libelle { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le stcok est obligatoire.")]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez saisir un nombre positif.")]
        public int Stock { get; set; }
        public bool EstDisponible => Stock > 0;
    }
}
