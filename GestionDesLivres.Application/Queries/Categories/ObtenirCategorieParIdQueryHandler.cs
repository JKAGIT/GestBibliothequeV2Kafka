using AutoMapper;
using GestionDesLivres.Application.DTO;
using GestionDesLivres.Application.Queries.Livres;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Categories
{
    public class ObtenirCategorieParIdQueryHandler : IRequestHandler<ObtenirCategorieParIdQuery, CategorieDto>
    {
        private readonly ICategorieRepository _categorieRepository;
        private readonly IMapper _mapper;
        private readonly IRecherche<Categorie> _recherche;

        public ObtenirCategorieParIdQueryHandler(ICategorieRepository categorieRepository, IMapper mapper, IRecherche<Categorie> recherche)
        {
            _categorieRepository = categorieRepository;
            _mapper = mapper;
            _recherche = recherche;
        }

        public async Task<CategorieDto> Handle(ObtenirCategorieParIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categorie = await _recherche.GetAll()
                                                .Where(c => c.ID == request.CategorieId)
                                                .Include(c => c.Livres)
                                                .FirstOrDefaultAsync(cancellationToken);
                if (categorie == null)
                {
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Categorie", request.CategorieId));
                }

                var categorieDto = new CategorieDto
                {
                    Id = categorie.ID,
                    Code = categorie.Code,
                    Libelle = categorie.Libelle,
                    Livres = categorie.Livres.Select(livre => new LivreDto
                    {
                        Id = livre.ID,
                        Titre = livre.Titre,
                        Auteur = livre.Auteur,
                        Stock = livre.Stock,
                        CategorieId = categorie.ID
                    }).ToList()
                };

                return categorieDto;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }
    }
}
