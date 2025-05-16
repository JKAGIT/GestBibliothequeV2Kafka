using GestionDesLivres.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Livres
{
    public class ObtenirTousLivresDisponibleQuery : IRequest<IEnumerable<LivreDto>>
    {
    }
}
