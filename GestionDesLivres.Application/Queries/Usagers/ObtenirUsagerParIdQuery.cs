using GestionDesLivres.Application.DTO;
using MediatR;
using System;

namespace GestionDesLivres.Application.Queries.Usagers
{
    public class ObtenirUsagerParIdQuery : IRequest<UsagerDto>
    {
        public Guid UsagerId { get; set; }

        public ObtenirUsagerParIdQuery(Guid usagerId)
        {
            UsagerId = usagerId;
        }
    }
}
