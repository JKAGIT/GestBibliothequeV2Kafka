using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using GestionDesLivres.Infrastructure.Persistence;
using GestionDesLivres.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionDesLivres.Application.Commands.Reservations
{
    public class EmprunterLivreReserveCommandHandler : IRequestHandler<EmprunterLivreReserveCommand, bool>
    {
        private readonly IEmpruntRepository _empruntRepository;
        private readonly ILivreRepository _livreRepository;
        private readonly IRecherche<Reservation> _recherche;
        private readonly IUnitOfWork _unitOfWork;


        public EmprunterLivreReserveCommandHandler(IReservationRepository reservationRepository,
            IEmpruntRepository empruntRepository,
            ILivreRepository livreRepository, IRecherche<Reservation> recherche, IUnitOfWork unitOfWork)
        {
            _empruntRepository = empruntRepository;
            _livreRepository = livreRepository;
            _recherche = recherche;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(EmprunterLivreReserveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var reservation = await _recherche.GetAll()
                                              .Include(r => r.Livre)
                                              .Include(r => r.Emprunt)
                                              .FirstOrDefaultAsync(r => r.ID == request.IDReservation);

                if (reservation == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Reservation", request.IDReservation));


                if (reservation.Livre == null || reservation.Livre.Stock <= 0)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("StockEpuise"));

                if (reservation.Annuler)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("ReservationAnnulee"));
                if (reservation.Emprunt != null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("ReservationDejaEmpruntee"));


                var emprunt = new Emprunt
                {
                    DateDebut = DateTime.Now,
                    DateRetourPrevue = reservation.DateRetourEstimee,
                    IDUsager = reservation.IDUsager,
                    IDLivre = reservation.IDLivre,
                    IDReservation = reservation.ID,
                };

                await _empruntRepository.AddAsync(emprunt);
                await _livreRepository.MettreAJourStock(reservation.IDLivre, -1);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur inattendue s'est produite.", ex);
            }
        }
    }

}
