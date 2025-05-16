using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Domain.Repositories
{
    public interface IEntityValidationService<T> where T : class
    {
        Task<bool> VerifierExistenceAsync(Expression<Func<T, bool>> predicate);
    }
}
