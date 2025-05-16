using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Common.Interfaces;

namespace GestionDesLivres.Infrastructure.Repositories
{
    public class RetourRepository : IRetourRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public RetourRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(Retour retour)
        {
            await _unitOfWork.Retours.AddAsync(retour);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.Retours.DeleteAsync(id);
        }

        public async Task<IEnumerable<Retour>> GetAllAsync()
        {
            return await _unitOfWork.Retours.GetAllAsync();
        }

        public async Task<Retour> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Retours.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Retour retour)
        {
            await _unitOfWork.Retours.UpdateAsync(retour);
        }
    }
}
