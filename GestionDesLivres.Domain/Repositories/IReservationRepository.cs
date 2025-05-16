using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;

namespace GestionDesLivres.Domain.Repositories
{
    public interface IReservationRepository:IGenericRepository<Reservation>
    {
        Task AnnulerReservation(Guid idReservation);
        Task AjouterEmpruntReservation(Guid idReservation);
    }
}

