using AutoMapper;
using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;

namespace GestionDesLivres.Application.Queries.Usagers
{
    public class ObtenirTousUsagersQueryHandler : IRequestHandler<ObtenirTousUsagersQuery, IEnumerable<UsagerDto>>
    {
        private readonly IUsagerRepository _usagerRepository;
        private readonly IMapper _mapper;
        public ObtenirTousUsagersQueryHandler(IUsagerRepository usagerRepository, IMapper mapper)
        {
            _usagerRepository = usagerRepository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<UsagerDto>> Handle(ObtenirTousUsagersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var usagers = await _usagerRepository.GetAllAsync();

                return usagers.Select(usager => new UsagerDto
                {
                    Id = usager.ID,
                    Nom = usager.Nom,
                    Prenoms = usager.Prenoms,
                    Courriel = usager.Courriel,
                    Telephone = usager.Telephone
                });
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }
    }
}
