using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Livres
{
    public class VerifierDisponibiliteLivreQuery : IRequest<bool>
    {
        public Guid Id { get; set; }

        public VerifierDisponibiliteLivreQuery(Guid id)
        {
            Id = id;
        }
    }
}
