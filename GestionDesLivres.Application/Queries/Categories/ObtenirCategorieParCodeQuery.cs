using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Entities;
using MediatR;
using System;

namespace GestionDesLivres.Application.Queries.Categories
{
    public class ObtenirCategorieParCodeQuery : IRequest<CategorieDto>
    {
        public string Code { get; set; }

        public ObtenirCategorieParCodeQuery(string code)
        {
            Code = code;
        }
    }
}

