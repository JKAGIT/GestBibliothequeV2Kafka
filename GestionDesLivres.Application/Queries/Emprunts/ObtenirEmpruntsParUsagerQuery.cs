using GestionDesLivres.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Emprunts
{
    public class ObtenirEmpruntsParUsagerQuery : IRequest<IEnumerable<EmpruntDto>>
    {
        public Guid UsagerId { get; set; }

        public ObtenirEmpruntsParUsagerQuery(Guid usagerId)
        {
            UsagerId = usagerId;
        }
    }
}
