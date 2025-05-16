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
    public class ObtenirReservationsParIdQueryHandler : IRequestHandler<ObtenirReservationsParIdQuery, ReservationDto>
    {
        private readonly IRecherche<Reservation> _recherche;
        private readonly IEmpruntRepository _empruntRepository;

        public ObtenirReservationsParIdQueryHandler(IRecherche<Reservation> recherche, IEmpruntRepository empruntRepository)
        {
            _recherche = recherche;
            _empruntRepository = empruntRepository;
        }

        public async Task<ReservationDto> Handle(ObtenirReservationsParIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reservation = await _recherche.GetAll()
                                      .Where(e => e.ID == request.Id)
                                      .Include(e => e.Usager)
                                      .Include(e => e.Livre)
                                      .FirstOrDefaultAsync(cancellationToken);

                if (reservation == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Reservation", request.Id));

                return new ReservationDto
                {
                    ID = reservation.ID,
                    IDUsager = reservation.IDUsager,
                    IDLivre = reservation.IDLivre,
                    DateDebut = reservation.DateDebut,
                    DateRetourEstimee = reservation.DateRetourEstimee,
                    NomCompletUsager = $"{reservation.Usager.Nom} {reservation.Usager.Prenoms}".Trim(),
                    TitreLivre = reservation.Livre.Titre,
                    LivreDisponible = reservation.Livre.Stock > 0 ? "OUI" : "NON",
                    StatutReservation = reservation.DateRetourEstimee >= DateTime.Now ? "À jour" : "En retard",
                    Annuler = reservation.Annuler
                };
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }

        }
    }
}
