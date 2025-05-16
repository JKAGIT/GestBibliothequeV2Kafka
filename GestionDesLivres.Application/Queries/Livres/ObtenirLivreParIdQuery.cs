using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Entities;
using MediatR;
using System;

namespace GestionDesLivres.Application.Queries.Livres
{
    public class ObtenirLivreParIdQuery : IRequest<LivreDto>
    {
        public Guid LivreId { get; set; }

        public ObtenirLivreParIdQuery(Guid livreId)
        {
            LivreId = livreId;
        }
    }
}
