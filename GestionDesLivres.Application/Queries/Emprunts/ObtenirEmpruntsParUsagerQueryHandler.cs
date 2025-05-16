using AutoMapper;
using GestionDesLivres.Application.DTO;
using GestionDesLivres.Application.Queries.Categories;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Emprunts
{
    public class ObtenirEmpruntsParUsagerQueryHandler : IRequestHandler<ObtenirEmpruntsParUsagerQuery, IEnumerable<EmpruntDto>> 
    {
        private readonly IRecherche<Emprunt> _recherche;

        public ObtenirEmpruntsParUsagerQueryHandler(IRecherche<Emprunt> recherche)
        {
            _recherche = recherche;
        }
        public async Task<IEnumerable<EmpruntDto>> Handle(ObtenirEmpruntsParUsagerQuery request, CancellationToken cancellationToken)
        {
            try
            { 
            var emprunts = await _recherche.GetAll()
                                   .Where(e => e.IDUsager == request.UsagerId)
                                   .Include(e => e.Usager)
                                   .Include(e => e.Livre)
                                   .Include(e => e.Retour)
                                   .ToListAsync(cancellationToken);

            var empruntsDto = emprunts.Select(emprunt => new EmpruntDto
            {
                ID = emprunt.ID,
                IDReservation = emprunt.IDReservation,
                DateDebut = emprunt.DateDebut,
                DateRetourPrevue = emprunt.DateRetourPrevue,
                IDUsager = emprunt.IDUsager,
                IDLivre = emprunt.IDLivre,
                NomCompletUsager = $"{emprunt.Usager.Nom} {emprunt.Usager.Prenoms}".Trim(),
                TitreLivre = emprunt.Livre.Titre,
                DateRetour = emprunt.Retour != null ? emprunt.Retour.DateRetour : (DateTime?)null,
                EstRendu = emprunt.EstRendu
            });

            return empruntsDto;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }

    }
}
