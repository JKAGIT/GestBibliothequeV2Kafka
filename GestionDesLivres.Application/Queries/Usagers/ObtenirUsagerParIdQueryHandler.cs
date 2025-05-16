using AutoMapper;
using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using GestionDesLivres.Infrastructure.Repositories;
//using GestionDesUsagers.Application.Queries.Usagers;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace GestionDesLivres.Application.Queries.Usagers
{
    public class ObtenirUsagerParIdQueryHandler : IRequestHandler<ObtenirUsagerParIdQuery, UsagerDto>
    {
        private readonly IMapper _mapper;
        private readonly IRecherche<Usager> _recherche;

        public ObtenirUsagerParIdQueryHandler(IMapper mapper, IRecherche<Usager> recherche)
        {
            _mapper = mapper;
            _recherche = recherche;
        }
        public async Task<UsagerDto> Handle(ObtenirUsagerParIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var usager = await _recherche.GetAll()
                    .Where(u => u.ID == request.UsagerId)
                    .Include(u => u.Emprunts.Where(e => !e.EstRendu))
                    .ThenInclude(e => e.Livre)
                    .FirstOrDefaultAsync(cancellationToken);


                if (usager == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Usager", request.UsagerId));

                var usagerDto = _mapper.Map<UsagerDto>(usager);

                usagerDto.Emprunts = usager.Emprunts.Select(emprunt => new EmpruntDto
                {
                    ID = emprunt.ID,
                    IDLivre = emprunt.IDLivre,
                    TitreLivre = emprunt.Livre.Titre,
                    DateDebut = emprunt.DateDebut,
                    DateRetourPrevue = emprunt.DateRetourPrevue,
                    EstRendu = emprunt.EstRendu
                }).ToList();

                return usagerDto;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }

    }
}
