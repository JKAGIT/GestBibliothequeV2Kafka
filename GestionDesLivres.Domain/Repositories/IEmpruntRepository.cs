using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;

namespace GestionDesLivres.Domain.Repositories
{
    public interface IEmpruntRepository : IGenericRepository<Emprunt>
    {
        Task<IEnumerable<Emprunt>> ObtenirEmpruntParUsager(Guid idUsager);
    }
}
