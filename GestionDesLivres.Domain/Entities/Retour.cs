using GestionDesLivres.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionDesLivres.Domain.Entities
{
    public class Retour : BaseEntity, IAggregateRoot
    {
        [Required]
        [ForeignKey("Emprunt")]
        public Guid IDEmprunt { get; set; }

        public Emprunt? Emprunt { get; set; }

        [Required]
        public DateTime DateRetour { get; set; }
    }
}
