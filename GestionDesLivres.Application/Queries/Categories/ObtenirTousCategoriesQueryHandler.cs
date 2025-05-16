using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestionDesLivres.Domain.Resources;
using GestionDesLivres.Domain.Exceptions;

namespace GestionDesLivres.Application.Queries.Categories
{
    public class ObtenirTousCategoriesQueryHandler : IRequestHandler<ObtenirTousCategoriesQuery, IEnumerable<CategorieDto>>
    {
        private readonly IRecherche<Categorie> _recherche;

        public ObtenirTousCategoriesQueryHandler(IRecherche<Categorie> recherche)
        {
            _recherche = recherche;
        }

        public async Task<IEnumerable<CategorieDto>> Handle(ObtenirTousCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _recherche.GetAll()
                                                  .Include(c => c.Livres)
                                                  .ToListAsync();

                var categoriesDto = categories.Select(categorie => new CategorieDto
                {
                    Id = categorie.ID,
                    Code = categorie.Code,
                    Libelle = categorie.Libelle,
                    Livres = categorie.Livres.Select(livre => new LivreDto
                    {
                        Id = livre.ID,
                        Titre = livre.Titre,
                        Auteur = livre.Auteur,
                        Stock = livre.Stock,
                        CategorieId = categorie.ID
                    })
                });
                return categoriesDto;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }
    }
}
