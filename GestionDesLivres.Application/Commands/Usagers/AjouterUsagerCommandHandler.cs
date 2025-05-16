using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;


namespace GestionDesLivres.Application.Commands.Usagers
{
    public class AjouterUsagerCommandHandler : IRequestHandler<AjouterUsagerCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService<Usager> _entityValidationService;
        private readonly IUsagerRepository _usagerRepository;
        public AjouterUsagerCommandHandler(IUnitOfWork unitOfWork, IEntityValidationService<Usager> entityValidationService, IUsagerRepository usagerRepository)
        {
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _usagerRepository = usagerRepository;
        }

        public async Task<Guid> Handle(AjouterUsagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> validationErrors = new List<string>();

                if (await _entityValidationService.VerifierExistenceAsync(u => u.Courriel == request.Courriel))
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EntiteExisteDeja", "Un usager", request.Courriel));

                var usager = new Usager
                {
                    ID = Guid.NewGuid(),
                    Nom = request.Nom,
                    Prenoms = request.Prenoms,
                    Courriel = request.Courriel,
                    Telephone = request.Telephone
                };
                if (!usager.EstValide())
                    throw new ValidationException(ErreurMessageProvider.GetMessage("DonneeInvalid"));

                await _usagerRepository.AddAsync(usager);
                await _unitOfWork.CompleteAsync();

                return usager.ID;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }
    }
}