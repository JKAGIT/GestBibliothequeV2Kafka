using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;


namespace GestionDesLivres.Application.Commands.Categories
{
    public class AjouterCategorieCommandHandler : IRequestHandler<AjouterCategorieCommand, Guid>
    {
        private readonly ICategorieRepository _categorieRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService<Categorie> _entityValidationService;

        public AjouterCategorieCommandHandler(ICategorieRepository categorieRepository, IUnitOfWork unitOfWork, IEntityValidationService<Categorie> entityValidationService)
        {
            _categorieRepository = categorieRepository;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
        }

        public async Task<Guid> Handle(AjouterCategorieCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> validationErrors = new List<string>();
                if (string.IsNullOrEmpty(request.Code) || string.IsNullOrEmpty(request.Libelle))
                    validationErrors.Add(ErreurMessageProvider.GetMessage("ValeurNulle"));

                if (await _entityValidationService.VerifierExistenceAsync(c => c.Code == request.Code))
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EntiteExisteDeja", "Une catégorie", request.Code));

                var categorie = new Categorie
                {
                    ID = Guid.NewGuid(),
                    Libelle = request.Libelle,
                    Code = request.Code
                };
                if (!categorie.EstValide())
                    throw new ValidationException(ErreurMessageProvider.GetMessage("DonneeInvalid"));

                await _categorieRepository.AddAsync(categorie);
                await _unitOfWork.CompleteAsync();

                return categorie.ID;
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
