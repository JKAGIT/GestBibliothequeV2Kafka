using AutoMapper;
using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Livres
{
    public class ObtenirLivreParIdQueryHandler : IRequestHandler<ObtenirLivreParIdQuery, LivreDto>
    {
        private readonly ILivreRepository _livreRepository;
        private readonly IMapper _mapper;

        public ObtenirLivreParIdQueryHandler(ILivreRepository livreRepository, IMapper mapper)
        {
            _livreRepository = livreRepository;
            _mapper = mapper;
        }

        public async Task<LivreDto> Handle(ObtenirLivreParIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var livres = await _livreRepository.ObtenirLivresAvecCategories(request.LivreId);
                var livre = livres
                    .AsQueryable()
                    .Include(l => l.Categorie)
                    .FirstOrDefault(l => l.ID == request.LivreId);
                if (livre == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Livre", request.LivreId));

                return new LivreDto
                {
                    Id = livre.ID,
                    Titre = livre.Titre,
                    Auteur = livre.Auteur,
                    CategorieId = livre.IDCategorie,
                    Libelle = livre?.Categorie?.Libelle,
                    Stock = livre.Stock
                };
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }

    }
}
