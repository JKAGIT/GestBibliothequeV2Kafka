using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Livres
{
    public class MettreAJourStockLivreCommandHandler : IRequestHandler<MettreAJourStockLivreCommand, bool>
    {
        private readonly ILivreRepository _livreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MettreAJourStockLivreCommandHandler(ILivreRepository livreRepository, IUnitOfWork unitOfWork)
        {
            _livreRepository = livreRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(MettreAJourStockLivreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var livre = await _livreRepository.GetByIdAsync(request.Id);
                if (livre == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Livre", request.Id));

                await _livreRepository.MettreAJourStock(request.Id, request.QuantiteAjoutee);
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
