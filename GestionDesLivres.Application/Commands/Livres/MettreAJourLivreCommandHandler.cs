using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;

namespace GestionDesLivres.Application.Commands.Livres
{
    public class MettreAJourLivreCommandHandler : IRequestHandler<MettreAJourLivreCommand, bool>
    {
        private readonly ILivreRepository _livreRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService<Livre> _entityValidationService;

        public MettreAJourLivreCommandHandler(ILivreRepository livreRepository, IUnitOfWork unitOfWork, IEntityValidationService<Livre> entityValidationService)
        {
            _livreRepository = livreRepository;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
        }

        public async Task<bool> Handle(MettreAJourLivreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> validationErrors = new List<string>();
                if (string.IsNullOrEmpty(request.Titre))
                    validationErrors.Add(ErreurMessageProvider.GetMessage("TitreRequis"));

                if (string.IsNullOrEmpty(request.Auteur))
                    validationErrors.Add(ErreurMessageProvider.GetMessage("AuteurRequis"));

                if (request.Stock < 0)
                    validationErrors.Add(ErreurMessageProvider.GetMessage("StockPositif"));

                if (validationErrors.Count > 0)
                    throw new ValidationException(validationErrors);


                var livre = await _livreRepository.GetByIdAsync(request.Id);
                if (livre == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Livre", request.Id));

                var categorie = await _unitOfWork.Categories.GetByIdAsync(request.CategorieId);
                if (categorie == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Categorie", request.CategorieId));

                if (await _entityValidationService.VerifierExistenceAsync(l => l.Titre == request.Titre && l.Auteur == request.Auteur && l.ID != request.Id))
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EntiteExisteDeja", "Un livre", request.Titre));

                livre.Titre = request.Titre;
                livre.Auteur = request.Auteur;
                livre.IDCategorie = request.CategorieId;
                livre.Stock = request.Stock;

                if (!livre.EstValide())
                    throw new ValidationException(ErreurMessageProvider.GetMessage("DonneeInvalid"));

                await _livreRepository.UpdateAsync(livre);
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
