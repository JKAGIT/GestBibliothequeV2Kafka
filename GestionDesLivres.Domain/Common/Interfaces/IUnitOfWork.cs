using GestionDesLivres.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Domain.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Livre> Livres { get; }
        IGenericRepository<Categorie> Categories { get; }
        IGenericRepository<Utilisateur> Utilisateurs { get; }
        IGenericRepository<Usager> Usagers { get; }
        IGenericRepository<Emprunt> Emprunts { get; }
        IGenericRepository<Retour> Retours { get; }
        IGenericRepository<Reservation> Reservations { get; }

        Task<int> CompleteAsync();
    }
}
