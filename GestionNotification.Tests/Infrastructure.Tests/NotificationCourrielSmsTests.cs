using FluentAssertions;
using GestionNotification.Domain.Entities;
using GestionNotification.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionNotification.Tests.Infrastructure.Tests
{
    public class NotificationCourrielSmsTests
    {
            [Fact]
            public async Task EnvoyerAsync_DevraitEnvoyerNotificationCourriel()
            {
                //// Arrange
                //var notification = Notification.CreationNotificationCourriel("Test message", NotificationType.LivreReserveDisponible, "test@example.com");
                //var service = new NotificationCourriel();

                //// Act - Aucune exception ne devrait être levée
                //Func<Task> act = async () => await service.EnvoyerAsync(notification);

                //// Assert
                //await act.Should().NotThrowAsync();
            }
        }

        public class NotificationSmsTests
        {
            [Fact]
            public async Task EnvoyerAsync_DevraitEnvoyerNotificationSms()
            {
                //// Arrange
                //var notification = Notification.CreationNotificationSms("Test message", NotificationType.LivreReserveDisponible, "4185899874");
                //var service = new NotificationSMS();

                //// Act - Aucune exception ne devrait être levée
                //Func<Task> act = async () => await service.EnvoyerAsync(notification);

                //// Assert
                //await act.Should().NotThrowAsync();
            }
        }

        public class NotificationInterneTests
        {
            [Fact]
            public async Task EnvoyerAsync_DevraitEnvoyerNotificationInterne()
            {
                //// Arrange
                //var notification = Notification.CreationNotificationInterne("Test message", NotificationType.LivreReserveDisponible, "test@example.com", "4185899874");
                //var service = new NotificationInterne();

                //// Act - Aucune exception ne devrait être levée
                //Func<Task> act = async () => await service.EnvoyerAsync(notification);

                //// Assert
                //await act.Should().NotThrowAsync();
            }
        }
}
