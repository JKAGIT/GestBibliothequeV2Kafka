using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Exceptions;
using GestionDesLivres.Domain.Repositories;
using MediatR;

namespace GestionDesLivres.Application.Commands.Reservations
{
    public class AjouterReservationCommandHandler : IRequestHandler<AjouterReservationCommand, Guid>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AjouterReservationCommandHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork)
        {
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AjouterReservationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var reservation = new Reservation
                {
                    IDUsager = request.IDUsager,
                    IDLivre = request.IDLivre,
                    DateDebut = request.DateDebut,
                    DateRetourEstimee = request.DateRetourEstimee
                };

                await _reservationRepository.AddAsync(reservation);
                await _unitOfWork.CompleteAsync();
                return reservation.ID;
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
