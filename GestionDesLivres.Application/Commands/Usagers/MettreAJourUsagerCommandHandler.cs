using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;

namespace GestionDesLivres.Application.Commands.Usagers
{
    public class MettreAJourUsagerCommandHandler : IRequestHandler<MettreAJourUsagerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsagerRepository _usagerRepository;
        public MettreAJourUsagerCommandHandler(IUnitOfWork unitOfWork, IUsagerRepository usagerRepository)
        {
            _unitOfWork = unitOfWork;
            _usagerRepository = usagerRepository;
        }

        public async Task<bool> Handle(MettreAJourUsagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> validationErrors = new List<string>();
                if (string.IsNullOrEmpty(request.Nom) || string.IsNullOrEmpty(request.Prenoms) || string.IsNullOrEmpty(request.Telephone) || string.IsNullOrEmpty(request.Courriel))
                    validationErrors.Add(ErreurMessageProvider.GetMessage("ValeurNulle"));

                var usager = await _unitOfWork.Usagers.GetByIdAsync(request.Id);
                if (usager == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Usager", request.Id));

                usager.Nom = request.Nom;
                usager.Prenoms = request.Prenoms;
                usager.Courriel = request.Courriel;
                usager.Telephone = request.Telephone;

                if (!usager.EstValide())
                    throw new ValidationException(ErreurMessageProvider.GetMessage("DonneeInvalid"));

                await _usagerRepository.UpdateAsync(usager);
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
