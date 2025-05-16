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
    public class EmpruntRepository : IEmpruntRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecherche<Emprunt> _recherche;
        public EmpruntRepository(IUnitOfWork unitOfWork, IRecherche<Emprunt> recherche)
        {
            _unitOfWork = unitOfWork;
            _recherche = recherche;
        }
        public async Task AddAsync(Emprunt emprunt)
        {
            await _unitOfWork.Emprunts.AddAsync(emprunt);
        }
        public async Task UpdateAsync(Emprunt emprunt)
        {
            await _unitOfWork.Emprunts.UpdateAsync(emprunt);
        }


        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Emprunts.DeleteAsync(id);
        }

        public async Task<IEnumerable<Emprunt>> GetAllAsync()
        {
            return await _unitOfWork.Emprunts.GetAllAsync();
        }

        public async Task<Emprunt> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Emprunts.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Emprunt>> ObtenirEmpruntParUsager(Guid idUsager)
        {
            return await _recherche.FindAsync(e => e.IDUsager == idUsager);
        }

        
    }
}
