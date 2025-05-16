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
    public class SupprimerEmpruntCommandHandler : IRequestHandler<SupprimerEmpruntCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupprimerEmpruntCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SupprimerEmpruntCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var emprunt = await _unitOfWork.Emprunts.GetByIdAsync(request.ID);
                if (emprunt == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Emprunt", request.ID));


                await _unitOfWork.Emprunts.DeleteAsync(request.ID);
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
