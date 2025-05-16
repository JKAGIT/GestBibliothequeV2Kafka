using System.ComponentModel.DataAnnotations;

namespace GestBibliotheque.Blazor.Models
{
    public class UsagerDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string Nom { get; set; }=string.Empty;
        [Required(ErrorMessage = "Le prenom est obligatoire.")]
        public string Prenoms { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le courriel est obligatoire.")]
        [EmailAddress(ErrorMessage = "Veuillez fournir un courriel valide.")]
        public string Courriel { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le téléphone est obligatoire.")]
        [RegularExpression(@"^(\d{3})\s?(\d{3})\s?(\d{4})$", ErrorMessage = "Le numéro de téléphone doit être au format 418 256 1234 ou 4182561234.")]
        [StringLength(12, ErrorMessage = "Le numéro de téléphone ne doit pas dépasser 12 caractères.")]
        [MinLength(10, ErrorMessage = "Le numéro de téléphone doit comporter au moins 10 chiffres.")]
        public string Telephone { get; set; } = string.Empty;
        public IEnumerable<EmpruntDto> Emprunts { get; set; } = new List<EmpruntDto>();        
    }
}
