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
    public class CategorieRepository : ICategorieRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecherche<Categorie> _recherche;

        public CategorieRepository(IUnitOfWork unitOfWork,IRecherche<Categorie> recherche)
        {
            _unitOfWork = unitOfWork;
            _recherche = recherche;
        }
        public async Task AddAsync(Categorie categorie)
        {
            await _unitOfWork.Categories.AddAsync(categorie);
        }

        public async Task UpdateAsync(Categorie categorie)
        {
            await _unitOfWork.Categories.UpdateAsync(categorie);
        }
        public async Task DeleteAsync(Guid idCategorie)
        {
            await _unitOfWork.Categories.DeleteAsync(idCategorie);
        }
        public async Task<Categorie> GetByIdAsync(Guid idCategorie)
        {
            var categorie = await _unitOfWork.Categories.GetByIdAsync(idCategorie);
            return categorie;
        }

        public async Task<IEnumerable<Categorie>> GetAllAsync()
        {
            try
            {
                return await _unitOfWork.Categories.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format($"Erreur lors de la recherche des {0}.", "Categories"), ex);
            }
        }

        public async Task<Categorie> ObtenirCategorieParCode(string code)
        {
            return await _recherche.GetAll().FirstOrDefaultAsync(c => c.Code == code);
        }

    }
}
