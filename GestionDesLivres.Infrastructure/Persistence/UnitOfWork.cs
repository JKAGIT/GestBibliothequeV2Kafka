using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GestBibliothequeContext _context;
        private IGenericRepository<Livre> _livre;
        private IGenericRepository<Categorie> _categorie;
        private IGenericRepository<Utilisateur> _utilisateur;
        private IGenericRepository<Usager> _usager;
        private IGenericRepository<Emprunt> _emprunt;
        private IGenericRepository<Retour> _retour;
        private IGenericRepository<Reservation> _reservation;

        public UnitOfWork(GestBibliothequeContext context)
        {
            _context = context;
            _livre = new GenericRepository<Livre>(_context);
            _categorie = new GenericRepository<Categorie>(_context);
            _utilisateur = new GenericRepository<Utilisateur>(_context);
            _usager = new GenericRepository<Usager>(_context);
            _emprunt = new GenericRepository<Emprunt>(_context);
            _retour = new GenericRepository<Retour>(_context);
            _reservation = new GenericRepository<Reservation>(_context);
        }
        public IGenericRepository<Livre> Livres => _livre;
        public IGenericRepository<Categorie> Categories => _categorie;
        public IGenericRepository<Utilisateur> Utilisateurs => _utilisateur;
        public IGenericRepository<Usager> Usagers => _usager;
        public IGenericRepository<Emprunt> Emprunts => _emprunt;
        public IGenericRepository<Retour> Retours => _retour;
        public IGenericRepository<Reservation> Reservations => _reservation;

        public async Task<int> CompleteAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
