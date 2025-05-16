using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;


namespace GestionDesLivres.Application.Commands.Livres
{
    public class AjouterLivreCommandHandler : IRequestHandler<AjouterLivreCommand, Guid>
    {
        private readonly ILivreRepository _livreRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService<Livre> _entityValidationService;

        public AjouterLivreCommandHandler(ILivreRepository livreRepository, IUnitOfWork unitOfWork, IEntityValidationService<Livre> entityValidationService)
        {
            _livreRepository = livreRepository;
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
        }
        public async Task<Guid> Handle(AjouterLivreCommand request, CancellationToken cancellationToken)
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


                var categorie = await _unitOfWork.Categories.GetByIdAsync(request.CategorieId);
                if (categorie == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Categorie", request.CategorieId));


                if (await _entityValidationService.VerifierExistenceAsync(l => l.Titre == request.Titre))
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EntiteExisteDeja", "Un livre", request.Titre));


                var livre = new Livre
                {
                    ID = Guid.NewGuid(),
                    Titre = request.Titre,
                    Auteur = request.Auteur,
                    IDCategorie = request.CategorieId,
                    Stock = request.Stock
                };
                if (!livre.EstValide())
                    throw new ValidationException(ErreurMessageProvider.GetMessage("DonneeInvalid"));

                await _livreRepository.AddAsync(livre);
                await _unitOfWork.CompleteAsync();

                return livre.ID;
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
