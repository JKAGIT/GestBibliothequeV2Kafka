using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using GestionDesLivres.Infrastructure.Repositories;
using MediatR;

namespace GestionDesLivres.Application.Commands.Usagers
{
    public class SupprimerUsagerCommandHandler : IRequestHandler<SupprimerUsagerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUsagerRepository _usagerRepository;
        private readonly IRecherche<Emprunt> _recherche;
        public SupprimerUsagerCommandHandler(IUnitOfWork unitOfWork, IUsagerRepository usagerRepository, IRecherche<Emprunt> recherche)
        {
            _unitOfWork = unitOfWork;
            _usagerRepository = usagerRepository;
            _recherche = recherche;
        }

        public async Task<bool> Handle(SupprimerUsagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usager = await _unitOfWork.Usagers.GetByIdAsync(request.Id);
                if (usager == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Usager", request.Id));


                var empruntsActifs = await _recherche.FindAsync(e => e.IDUsager == request.Id && !e.EstRendu);
                if (empruntsActifs.Any())
                {
                    throw new ValidationException(ErreurMessageProvider.GetMessage("ErreurSuppressionEntiteLiee", "un emprunt", "usager"));
                }

                await _usagerRepository.DeleteAsync(request.Id);
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
