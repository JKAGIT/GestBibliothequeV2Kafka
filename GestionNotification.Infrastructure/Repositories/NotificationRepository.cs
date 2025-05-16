
using GestionNotification.Domain.Entities;
using GestionNotification.Domain.Exceptions;
using GestionNotification.Domain.Repositories;
using GestionNotification.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace GestionNotification.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationContext _context;

        public NotificationRepository(NotificationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        //Queries
        public async Task<Notification> GetByIdAsync(Guid id)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(n => n.ID == id);
            if (notification == null)
                throw new NotificationException(id);
            return notification;
        }
       
        public async Task<IEnumerable<Notification>> GetAllAsync(DateTime? fromDate = null)
        {
            var query = _context.Notifications.AsQueryable();
            if (fromDate.HasValue)
            {
                query = query.Where(n => n.CreatedAt >= fromDate.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetByFilterAsync(Expression<Func<Notification, bool>> filter)
        {
            return await _context.Notifications.Where(filter).ToListAsync();

        }

        //Commands
        public async Task AddAsync(Notification notification)
        {
            if (notification == null || string.IsNullOrWhiteSpace(notification.Message))
            {
               Console.WriteLine("Le champ 'Message' est obligatoire et ne peut être vide.");
            }

                await _context.Notifications.AddAsync(notification);
            await CommitAsync();
        }

        public async Task UpdateAsync(Notification notification)
        {
            _context.Notifications.Update(notification);
            await CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var notification = await GetByIdAsync(id);
            _context.Notifications.Remove(notification);
            await CommitAsync();
        }
        private async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
