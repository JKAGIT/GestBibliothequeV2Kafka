using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Categories
{
    public class MettreAJourCategorieCommandHandler : IRequestHandler<MettreAJourCategorieCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategorieRepository _categorieRepository;
        public MettreAJourCategorieCommandHandler(IUnitOfWork unitOfWork, ICategorieRepository categorieRepository)
        {
            _unitOfWork = unitOfWork;
            _categorieRepository = categorieRepository;
        }
        public async Task<bool> Handle(MettreAJourCategorieCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> validationErrors = new List<string>();
                if (string.IsNullOrEmpty(request.Code) || string.IsNullOrEmpty(request.Libelle))
                    validationErrors.Add(ErreurMessageProvider.GetMessage("ValeurNulle"));

                var categorie = await _unitOfWork.Categories.GetByIdAsync(request.Id);
                if (categorie == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Categorie", request.Id));

                categorie.Code = request.Code;
                categorie.Libelle = request.Libelle;

                if (!categorie.EstValide())
                    throw new ValidationException(ErreurMessageProvider.GetMessage("DonneeInvalid"));

                await _categorieRepository.UpdateAsync(categorie);
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
