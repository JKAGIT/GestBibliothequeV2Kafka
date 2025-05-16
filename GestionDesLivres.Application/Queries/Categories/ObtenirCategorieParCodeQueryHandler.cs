using AutoMapper;
using GestionDesLivres.Application.DTO;
using GestionDesLivres.Application.Queries.Livres;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Categories
{
    public class ObtenirCategorieParCodeQueryHandler : IRequestHandler<ObtenirCategorieParCodeQuery, CategorieDto>
    {
        private readonly ICategorieRepository _categorieRepository;
        private readonly IMapper _mapper;

        public ObtenirCategorieParCodeQueryHandler(ICategorieRepository categorieRepository, IMapper mapper)
        {
            _categorieRepository = categorieRepository;
            _mapper = mapper;
        }

        public async Task<CategorieDto> Handle(ObtenirCategorieParCodeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categorie = await _categorieRepository.ObtenirCategorieParCode(request.Code);
                if (categorie == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Categorie", request.Code));
                return _mapper.Map<CategorieDto>(categorie);
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }
    }
}
