using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionDesLivres.Domain.Common.Interfaces;

namespace GestionDesLivres.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReservationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(Reservation reservation)
        {
            await _unitOfWork.Reservations.AddAsync(reservation);
        }

        public async Task AnnulerReservation(Guid idReservation)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(idReservation);
            await _unitOfWork.Reservations.UpdateAsync(reservation);
        }
        public async Task UpdateAsync(Reservation reservation)
        {
            await _unitOfWork.Reservations.UpdateAsync(reservation);
        }
        public async Task AjouterEmpruntReservation(Guid idReservation)
        {
            var reservation = await _unitOfWork.Reservations.GetByIdAsync(idReservation);

            var emprunt = new Emprunt
            {
                DateDebut = DateTime.Now,
                DateRetourPrevue = reservation.DateRetourEstimee,
                IDUsager = reservation.IDUsager,
                IDLivre = reservation.IDLivre,
                IDReservation = reservation.ID,
            };

            await _unitOfWork.Emprunts.AddAsync(emprunt);
            //Maj stock -1
            reservation.Emprunt = emprunt;

            await _unitOfWork.Reservations.UpdateAsync(reservation);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<Reservation> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Reservations.GetByIdAsync(id);
        }



        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Reservations.DeleteAsync(id);

        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _unitOfWork.Reservations.GetAllAsync();
        }


    }
}
