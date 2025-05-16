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
    public class EntityValidationService<T> : IEntityValidationService<T> where T : class
    {
        private readonly GestBibliothequeContext _context;
        private readonly DbSet<T> _dbSet;

        public EntityValidationService(GestBibliothequeContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<bool> VerifierExistenceAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var dbSet = _context.Set<T>();

            return await dbSet.AnyAsync(predicate);
        }
    }
}
