using GestionDesLivres.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Livres
{
    public class ObtenirLivresParCategorieQuery : IRequest<IEnumerable<LivreDto>>
    {
        public Guid CategorieId { get; set; }
        public ObtenirLivresParCategorieQuery(Guid categorieId)
        {
            CategorieId = categorieId;
        }
    }
}
