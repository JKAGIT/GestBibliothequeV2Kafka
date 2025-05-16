
using GestionNotification.Domain.Entities;
using System.Linq.Expressions;

namespace GestionNotification.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> GetByIdAsync(Guid id);
        Task<IEnumerable<Notification>> GetAllAsync(DateTime? fromDate = null);
        Task<IEnumerable<Notification>> GetByFilterAsync(Expression<Func<Notification, bool>> filter);
        Task AddAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(Guid id);

    }
}
