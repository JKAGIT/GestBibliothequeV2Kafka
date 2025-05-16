using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Livres
{
    public class VerifierDisponibiliteLivreQueryHandler : IRequestHandler<VerifierDisponibiliteLivreQuery, bool>
    {
        private readonly ILivreRepository _livreRepository;

        public VerifierDisponibiliteLivreQueryHandler(ILivreRepository livreRepository)
        {
            _livreRepository = livreRepository;
        }

        public async Task<bool> Handle(VerifierDisponibiliteLivreQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var livre = await _livreRepository.GetByIdAsync(request.Id);

                if (livre == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Livre", request.Id));

                return await _livreRepository.EstDisponible(request.Id);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }
    }
}
