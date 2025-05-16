using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Domain.Repositories
{
    public interface IRecherche<T> where T : class
    {
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(); 
    }
}
