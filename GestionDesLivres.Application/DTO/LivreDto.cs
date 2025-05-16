using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.DTO
{
    public class LivreDto
    {
        public Guid Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public Guid CategorieId { get; set; }
        public string Libelle { get; set; }
        public int Stock { get; set; }
        public bool EstDisponible => Stock > 0;
    }
}
