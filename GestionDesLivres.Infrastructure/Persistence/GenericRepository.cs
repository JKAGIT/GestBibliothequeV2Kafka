using GestionDesLivres.Domain.Common;
using GestionDesLivres.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Infrastructure.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, IAggregateRoot
    {
        private readonly GestBibliothequeContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(GestBibliothequeContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            var entry = _context.Entry(entity);
            var keyValues = entry.Metadata.FindPrimaryKey().Properties
                .Select(p => entry.Property(p.Name).CurrentValue).ToArray();

            var tracked = _context.Set<T>().Local.FirstOrDefault(e =>
                _context.Entry(e).Metadata.FindPrimaryKey().Properties
                    .Select(p => _context.Entry(e).Property(p.Name).CurrentValue)
                    .SequenceEqual(keyValues));

            if (tracked != null && tracked != entity)
            {
                _context.Entry(tracked).State = EntityState.Detached;
            }

            _dbSet.Update(entity);

        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
            
        }

    }
}