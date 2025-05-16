using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Categories
{
    public class SupprimerCategorieCommandHandler : IRequestHandler<SupprimerCategorieCommand, bool>
    {
        private readonly ICategorieRepository _categorieRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecherche<Categorie> _recherche;

        public SupprimerCategorieCommandHandler(ICategorieRepository categorieRepository, IUnitOfWork unitOfWork, IRecherche<Categorie> recherche)
        {
            _categorieRepository = categorieRepository;
            _unitOfWork = unitOfWork;
            _recherche = recherche;
        }
        public async Task<bool> Handle(SupprimerCategorieCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var categorieASupprimer = await _recherche.GetAll()
                                                 .Include(c => c.Livres)
                                                 .FirstOrDefaultAsync(c => c.ID == request.Id);

                if (categorieASupprimer == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Categorie", request.Id));

                if (categorieASupprimer.Livres != null && categorieASupprimer.Livres.Any())
                    throw new ValidationException(ErreurMessageProvider.GetMessage("ErreurSuppressionEntiteLiee", "une catégorie", "livres"));

                await _categorieRepository.DeleteAsync(request.Id);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur inattendue s'est produite.", ex);
            }
        }
    }
}
