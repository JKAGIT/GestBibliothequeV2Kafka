using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Retours.Commands
{
    public class SupprimerRetourCommandHandler : IRequestHandler<SupprimerRetourCommand, bool>
    {
        private readonly IRetourRepository _retourRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupprimerRetourCommandHandler(IRetourRepository retourRepository, IUnitOfWork unitOfWork)
        {
            _retourRepository = retourRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SupprimerRetourCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var retour = await _retourRepository.GetByIdAsync(request.ID);
                if (retour == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Retour", request.ID));

                var emprunt = await _unitOfWork.Emprunts.GetByIdAsync(retour.IDEmprunt);
                if (emprunt != null)
                {
                    return false;
                    throw new ValidationException(ErreurMessageProvider.GetMessage("ErreurSuppressionEntiteLiee", "un retour", "emprunt"));
                }

                await _retourRepository.DeleteAsync(request.ID);
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
