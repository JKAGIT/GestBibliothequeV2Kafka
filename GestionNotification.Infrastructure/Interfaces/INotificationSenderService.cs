using GestionNotification.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GestionNotification.Application.Interfaces
{
    public interface INotificationSenderService
    {
        Task EnvoyerNotificationAsync(Notification notification);
    }
}