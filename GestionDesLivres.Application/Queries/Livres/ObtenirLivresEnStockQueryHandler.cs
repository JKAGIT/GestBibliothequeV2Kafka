using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Livres
{
    public class ObtenirLivresEnStockQueryHandler : IRequestHandler<ObtenirLivresEnStockQuery, IEnumerable<LivreDto>>
    {
        private readonly ILivreRepository _livreRepository;

        public ObtenirLivresEnStockQueryHandler(ILivreRepository livreRepository)
        {
            _livreRepository = livreRepository;
        }

        public async Task<IEnumerable<LivreDto>> Handle(ObtenirLivresEnStockQuery request, CancellationToken cancellationToken)
        {
            try { 
            var livres = await _livreRepository.ObtenirLivresEnStock();

            return livres.Select(livre => new LivreDto
            {
                Id = livre.ID,
                Titre = livre.Titre,
                Auteur = livre.Auteur,
                CategorieId = livre.IDCategorie,
                Stock = livre.Stock
            });
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }

        }

    }
}
