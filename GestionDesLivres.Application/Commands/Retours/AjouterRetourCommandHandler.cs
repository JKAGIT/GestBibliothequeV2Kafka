using GestionDesLivres.Application.Commands.Categories;
using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Retours
{
    public class AjouterRetourCommandHandler : IRequestHandler<AjouterRetourCommand, Guid>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILivreRepository _livreRepository;

        public AjouterRetourCommandHandler(IUnitOfWork unitOfWork, ILivreRepository livreRepository)
        {
            _unitOfWork = unitOfWork;
            _livreRepository = livreRepository;
        }

        public async Task<Guid> Handle(AjouterRetourCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var emprunt = await _unitOfWork.Emprunts.GetByIdAsync(request.IDEmprunt);
                if (emprunt == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Emprunt", request.IDEmprunt));

                if (emprunt.EstRendu)
                    throw new ValidationException("Emprunt déjà rendu.");

                if (request.DateRetour == default)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("DateValide", request.DateRetour));

                var retour = new Retour
                {
                    ID = Guid.NewGuid(),
                    IDEmprunt = emprunt.ID,
                    DateRetour = DateTime.UtcNow
                };
                emprunt.EstRendu = true;

                await _unitOfWork.Retours.AddAsync(retour);
                await _livreRepository.MettreAJourStock(emprunt.IDLivre, 1);
                await _unitOfWork.Emprunts.UpdateAsync(emprunt);
                await _unitOfWork.CompleteAsync();

                return retour.ID;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }
    }
}
