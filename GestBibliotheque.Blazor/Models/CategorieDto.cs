using System.ComponentModel.DataAnnotations;

namespace GestBibliotheque.Blazor.Models
{
    public class CategorieDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Le Code est obligatoire.")]
        [StringLength(4, ErrorMessage = "Le Code doit être exactement de 4 caractères.")]
        [RegularExpression(@"^[a-zA-Z0-9]{4}$", ErrorMessage = "Le Code doit contenir uniquement 4 caractères alphanumériques.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Le Libellé est obligatoire.")]
        [StringLength(80, ErrorMessage = "Le Libellé ne doit pas dépasser 100 caractères.")]
        public string Libelle { get; set; }
        public IEnumerable<LivreDto> Livres { get; set; } = new List<LivreDto>();
    }
}
