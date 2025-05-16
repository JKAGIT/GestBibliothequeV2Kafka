using Azure.Core;
using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestionDesLivres.Infrastructure.Repositories
{
    public class LivreRepository : ILivreRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityValidationService<Livre> _entityValidationService;
        private readonly IRecherche<Livre> _rechercheLivre;

        public LivreRepository(IUnitOfWork unitOfWork, IEntityValidationService<Livre> entityValidationService, IRecherche<Livre> recherche)
        {
            _unitOfWork = unitOfWork;
            _entityValidationService = entityValidationService;
            _rechercheLivre = recherche;

        }
        public async Task AddAsync(Livre livre)
        {
            await _unitOfWork.Livres.AddAsync(livre);
        }

        public async Task UpdateAsync(Livre livre)
        {
            await _unitOfWork.Livres.UpdateAsync(livre);
        }

        public async Task DeleteAsync(Guid idLivre)
        {
            await _unitOfWork.Livres.DeleteAsync(idLivre);
        }

        public async Task<bool> EstDisponible(Guid idLivre)
        {
            var livre = await _unitOfWork.Livres.GetByIdAsync(idLivre);
            return livre != null && livre.Stock > 0;
        }

        public async Task MettreAJourStock(Guid idLivre, int nouveauStock)
        {
            var livre = await _unitOfWork.Livres.GetByIdAsync(idLivre);
            livre.Stock += nouveauStock;
            await _unitOfWork.Livres.UpdateAsync(livre);
        }

        public async Task<IEnumerable<Livre>> GetAllAsync()  //lazy loading
        {

            return await _unitOfWork.Livres.GetAllAsync();
        }

        public async Task<Livre> GetByIdAsync(Guid idLivre)
        {
            var livre = await _unitOfWork.Livres.GetByIdAsync(idLivre);
            return livre;
        }

        public async Task<IEnumerable<Livre>> ObtenirLivresAvecCategories(Guid? id = null)
        {
            try
            {
                IQueryable<Livre> query = _rechercheLivre.GetAll().Include(l => l.Categorie);

                if (id.HasValue)
                {
                    query = query.Where(l => l.ID == id.Value);
                }
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format($"Erreur lors de la recherche des {0}.", "Livres"), ex);
            }
        }

        public async Task<IEnumerable<Livre>> ObtenirLivresEnStock()
        {
            return await _rechercheLivre.FindAsync(l => l.Stock > 0);
        }

        public async Task<IEnumerable<Livre>> ObtenirLivresParCategorie(Guid idCategorie)
        {
            return await _rechercheLivre.FindAsync(l => l.IDCategorie == idCategorie);
        }

    }
}
