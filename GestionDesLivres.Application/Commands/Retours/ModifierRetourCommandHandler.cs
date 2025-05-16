using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using global::GestionDesLivres.Application.Retours.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Retours.Commands
{
    public class ModifierRetourCommandHandler : IRequestHandler<ModifierRetourCommand, bool>
    {
        private readonly IRetourRepository _retourRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ModifierRetourCommandHandler(IRetourRepository retourRepository, IUnitOfWork unitOfWork)
        {
            _retourRepository = retourRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ModifierRetourCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var retour = await _retourRepository.GetByIdAsync(request.ID);
                if (retour == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Retour", request.ID));

                retour.DateRetour = request.DateRetour;

                await _retourRepository.UpdateAsync(retour);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }
    }
}