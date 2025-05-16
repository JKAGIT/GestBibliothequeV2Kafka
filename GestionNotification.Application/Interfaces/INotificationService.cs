
using GestionNotification.Domain.Entities;
using GestionNotification.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GestionNotification.Application.Interfaces
{
    public interface INotificationService
    {
        // 🔹 Récupération des notifications
        Task<Notification> GetByIdAsync(Guid id);
        Task<IEnumerable<Notification>> GetAllAsync(DateTime? fromDate = null);
        Task<IEnumerable<Notification>> GetByFilterAsync(Expression<Func<Notification, bool>> filter);       

        // 🔹 Modification des notifications
        Task AddAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(Guid id);

        // 🔹 Gestion et envoi des notifications
        Task SendNotificationAsync(Notification notification);
        Task RetryFailedNotificationsAsync();
    }
}