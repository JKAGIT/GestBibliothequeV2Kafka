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
    public class AnnulerReservationCommandHandler : IRequestHandler<AnnulerReservationCommand, bool>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AnnulerReservationCommandHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AnnulerReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> validationErrors = new List<string>();

                var reservation = await _reservationRepository.GetByIdAsync(request.IDReservation);
                if (reservation == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Reservation", request.IDReservation));

                reservation.Annuler = true;
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
