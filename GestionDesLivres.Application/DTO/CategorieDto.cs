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
    public class CategorieDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }
        public IEnumerable<LivreDto> Livres { get; set; } =  new List<LivreDto>();
    }
}
