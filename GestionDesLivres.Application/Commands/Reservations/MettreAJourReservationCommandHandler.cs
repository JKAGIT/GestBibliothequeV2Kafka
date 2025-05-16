using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Reservations
{
    public class MettreAJourReservationCommandHandler : IRequestHandler<MettreAJourReservationCommand, bool>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MettreAJourReservationCommandHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(MettreAJourReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> validationErrors = new List<string>();
                if (request.IDLivre == Guid.Empty || request.IDUsager == Guid.Empty)
                    validationErrors.Add(ErreurMessageProvider.GetMessage("ValeurNulle"));

                var reservation = await _reservationRepository.GetByIdAsync(request.Id);
                if (reservation == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Reservation", request.Id));

                reservation.DateRetourEstimee = request.DateRetourEstimee;
                reservation.IDLivre = request.IDLivre;
                reservation.IDUsager = request.IDUsager;
                reservation.DateDebut = request.DateDebut;
                reservation.Annuler = request.Annuler;
                await _reservationRepository.UpdateAsync(reservation);
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
