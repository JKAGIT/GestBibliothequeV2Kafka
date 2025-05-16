using AutoMapper;
using GestionDesLivres.Application.DTO;
using GestionDesLivres.Application.Queries.Categories;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using GestionDesLivres.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Emprunts
{
    public class ObtenirEmpruntsParIdQueryHandler : IRequestHandler<ObtenirEmpruntsParIdQuery, EmpruntDto>
    {
        private readonly IRecherche<Emprunt> _recherche;

        public ObtenirEmpruntsParIdQueryHandler(IRecherche<Emprunt> recherche)
        {
            _recherche = recherche;
        }


        public async Task<EmpruntDto> Handle(ObtenirEmpruntsParIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var emprunt = await _recherche.GetAll()
                                      .Where(e => e.ID == request.Id)
                                      .Include(e => e.Usager)
                                      .Include(e => e.Livre)
                                      .Include(e => e.Retour)
                                      .FirstOrDefaultAsync(cancellationToken);

                if (emprunt == null)
                    return null;

                return new EmpruntDto
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
                };
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }

    }
}
