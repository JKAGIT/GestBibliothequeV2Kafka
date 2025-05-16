using GestionNotification.Domain.Entities;
using GestionNotification.Domain.Enums;
using GestionNotification.Domain.Repositories;
using GestionNotification.Infrastructure.Repositories;
using Moq;
using FluentAssertions;


namespace GestionNotification.Tests.Infrastructure.Tests
{
    public class NotificationProcessingTests
    {
        [Fact]
        public async Task TraiterNotificationsAsync_DevraitEnvoyerToutesLesNotificationsPending()
        {
            //// Arrange
            //var typeDeNotification = NotificationType.LivreReserveDisponible;
            //var notification1 = Notification.CreationNotificationCourriel("Message 1", typeDeNotification, "test1@example.com");
            //var notification2 = Notification.CreationNotificationSms("Message 2", typeDeNotification, "4182552525");
            //var pendingNotifications = new List<Notification> { notification1, notification2 };

            //var mockRepository = new Mock<INotificationRepository>();
            //mockRepository.Setup(r => r.GetPendingNotificationsAsync())
            //    .ReturnsAsync(pendingNotifications);

            //var mockUnitOfWork = new Mock<IUnitOfWork>();
            //mockUnitOfWork.Setup(u => u.CompleteAsync())
            //    .ReturnsAsync(2); 

            //var dispatcher = new NotificationDispatcher(mockRepository.Object, mockUnitOfWork.Object);

            //// Act
            //await dispatcher.TraiterNotificationsAsync();

            //// Assert
            //mockRepository.Verify(r => r.GetPendingNotificationsAsync(), Times.Once);
            //mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);

            //// Vérifier que les notifications ont été mises à jour
            //notification1.Status.Should().Be(NotificationStatus.Sent);
            //notification2.Status.Should().Be(NotificationStatus.Sent);
        }

        [Fact]
        public async Task TraiterNotificationsAsync_DevraitGererLesExceptions()
        {
            //// Arrange
            //var typeDeNotification = NotificationType.LivreReserveDisponible;
            //var notification1 = Notification.CreationNotificationCourriel("Message 1", typeDeNotification, "test1@example.com");
            //var notification2 = Notification.CreationNotificationCourriel("Message 2", typeDeNotification, string.Empty);
            //var pendingNotifications = new List<Notification> { notification1, notification2 };

            //var mockRepository = new Mock<INotificationRepository>();
            //mockRepository.Setup(r => r.GetPendingNotificationsAsync())
            //    .ReturnsAsync(pendingNotifications);

            //var mockUnitOfWork = new Mock<IUnitOfWork>();
            //mockUnitOfWork.Setup(u => u.CompleteAsync())
            //    .ReturnsAsync(2); // 2 modifications enregistrées

            //var dispatcher = new NotificationDispatcher(mockRepository.Object, mockUnitOfWork.Object);

            //// Act & Assert
            //await dispatcher.TraiterNotificationsAsync();

            //// La première notification devrait être envoyée avec succès
            //notification1.Status.Should().Be(NotificationStatus.Sent);

            //// La seconde notification devrait être marquée comme échouée
            //notification2.Status.Should().Be(NotificationStatus.Failed);
            //notification2.RetryCount.Should().Be(1);
            //notification2.ErrorMessage.Should().NotBeNull();
        }
    }

}
