using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Common.Interfaces;
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
    public class ObtenirLivresParCategorieQueryHandler : IRequestHandler<ObtenirLivresParCategorieQuery, IEnumerable<LivreDto>>
    {
        private readonly ILivreRepository _livreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ObtenirLivresParCategorieQueryHandler(ILivreRepository livreRepository, IUnitOfWork unitOfWork)
        {
            _livreRepository = livreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LivreDto>> Handle(ObtenirLivresParCategorieQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categorie = await _unitOfWork.Categories.GetByIdAsync(request.CategorieId);
                if (categorie == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Categorie", request.CategorieId));

                var livres = await _livreRepository.ObtenirLivresParCategorie(request.CategorieId);

                return livres.Select(livre => new LivreDto
                {
                    Id = livre.ID,
                    Titre = livre.Titre,
                    Auteur = livre.Auteur,
                    CategorieId = livre.IDCategorie,
                    Libelle = categorie.Libelle,
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
