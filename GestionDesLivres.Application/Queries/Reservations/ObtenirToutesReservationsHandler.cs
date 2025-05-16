using GestionDesLivres.Application.DTO;
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

namespace GestionDesLivres.Application.Queries.Reservations
{
    public class ObtenirToutesReservationsHandler : IRequestHandler<ObtenirToutesReservationsQuery, IEnumerable<ReservationDto>>
    {

        private readonly IRecherche<Reservation> _recherche;

        public ObtenirToutesReservationsHandler(IRecherche<Reservation> recherche)
        {
            _recherche = recherche;
        }

        public async Task<IEnumerable<ReservationDto>> Handle(ObtenirToutesReservationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reservations = await _recherche.GetAll()
                                      .Where(r => !r.Annuler && r.Emprunt == null)
                                      .Include(r => r.Usager)
                                      .Include(r => r.Livre)
                                      .ToListAsync(cancellationToken);

                return reservations.Select(reservation => new ReservationDto
                {
                    ID = reservation.ID,
                    IDUsager = reservation.IDUsager,
                    IDLivre = reservation.IDLivre,
                    DateDebut = reservation.DateDebut,
                    DateRetourEstimee = reservation.DateRetourEstimee,
                    Annuler = reservation.Annuler,
                    NomCompletUsager = $"{reservation.Usager.Nom} {reservation.Usager.Prenoms}".Trim(),
                    TitreLivre = reservation.Livre.Titre,
                    LivreDisponible = reservation.Livre.Stock > 0 ? "OUI" : "NON",
                    StatutReservation = reservation.DateRetourEstimee >= DateTime.Now ? "À jour" : "En retard",
                    EstEmprunte = reservation.Emprunt != null
                });
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ErreurMessageProvider.GetMessage(ex.Message));
            }
        }
    }
}
