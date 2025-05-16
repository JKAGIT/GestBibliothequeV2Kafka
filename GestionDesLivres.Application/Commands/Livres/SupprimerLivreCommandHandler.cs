using GestionDesLivres.Application.Commands.Livres;
using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using GestionDesLivres.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Categories
{
    public class SupprimerLivreCommandHandler : IRequestHandler<SupprimerLivreCommand, bool>
    {
        private readonly ILivreRepository _livreRepository;
        private readonly IRecherche<Emprunt> _recherche;
        private readonly IUnitOfWork _unitOfWork;

        public SupprimerLivreCommandHandler(ILivreRepository livreRepository, IUnitOfWork unitOfWork, IRecherche<Emprunt> recherche)
        {
            _livreRepository = livreRepository;
            _unitOfWork = unitOfWork;
            _recherche = recherche;
        }

        public async Task<bool> Handle(SupprimerLivreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var livre = await _livreRepository.GetByIdAsync(request.Id);
                if (livre == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Livre", request.Id));

                var empruntsActifs = await _recherche.FindAsync(e => e.IDLivre == request.Id && !e.EstRendu);

                if (empruntsActifs.Any())
                    throw new ValidationException(ErreurMessageProvider.GetMessage("ErreurSuppressionEntiteLiee", "un emprunt", "livre"));


                await _livreRepository.DeleteAsync(request.Id);
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
