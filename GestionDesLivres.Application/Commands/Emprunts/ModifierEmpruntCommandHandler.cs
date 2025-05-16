using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Emprunts
{
    public class ModifierEmpruntCommandHandler : IRequestHandler<ModifierEmpruntCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModifierEmpruntCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ModifierEmpruntCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var emprunt = await _unitOfWork.Emprunts.GetByIdAsync(request.ID);
                if (emprunt == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Emprunt", request.ID));

                if (request.DateDebut == default || request.DateRetourPrevue == default)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("DateValide", request.DateDebut));

                emprunt.DateRetourPrevue = request.DateRetourPrevue;
                emprunt.DateDebut = request.DateDebut;

                await _unitOfWork.Emprunts.UpdateAsync(emprunt);
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
