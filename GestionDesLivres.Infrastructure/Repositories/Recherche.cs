using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Infrastructure.Repositories
{
    public class Recherche<T> : IRecherche<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        public Recherche(GestBibliothequeContext context)
        {
            _dbSet = context.Set<T>();
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }
    }
}
