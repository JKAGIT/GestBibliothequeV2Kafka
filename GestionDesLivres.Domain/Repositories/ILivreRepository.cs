using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Domain.Repositories
{
    public interface ILivreRepository : IGenericRepository<Livre>
    {
        Task MettreAJourStock(Guid idLivre, int nouveauStock);
        Task<bool> EstDisponible(Guid idLivre);
        Task<IEnumerable<Livre>> ObtenirLivresParCategorie(Guid idCategorie);
        Task<IEnumerable<Livre>> ObtenirLivresEnStock();
        Task<IEnumerable<Livre>> ObtenirLivresAvecCategories(Guid? id = null);
    }
}
