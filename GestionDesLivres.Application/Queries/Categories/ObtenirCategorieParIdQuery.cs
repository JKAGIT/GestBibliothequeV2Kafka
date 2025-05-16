using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Entities;
using MediatR;
using System;

namespace GestionDesLivres.Application.Queries.Categories
{
    public class ObtenirCategorieParIdQuery : IRequest<CategorieDto>
    {
        public Guid CategorieId { get; set; }

        public ObtenirCategorieParIdQuery(Guid categorieId)
        {
            CategorieId = categorieId;
        }
    }
}

