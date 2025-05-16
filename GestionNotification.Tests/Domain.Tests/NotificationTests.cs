using GestionNotification.Domain.Entities;
using GestionNotification.Domain.Enums;
using FluentAssertions;

namespace GestionNotification.Tests.Domain.Tests
{
    public class NotificationTests
    {
        [Fact]
        public void CreationNotificationCourriel_DevraitCreerUneNotificationValide()
        {
            // Arrange
            string message ="Test message";
            string courriel = "test@example.com";
            var typeDeNotification = NotificationType.LivreReserveDisponible;

            // Act
            //var notification = Notification.CreationNotificationCourriel(message, typeDeNotification, courriel);

            //// Assert
            //notification.Message.Should().Be(message);
            //notification.Message.Should().NotBeNullOrWhiteSpace();
            //notification.Courriel.Should().Be(courriel);
            //notification.Courriel.Should().NotBeNullOrWhiteSpace();
            //notification.Type.Should().Be(typeDeNotification);
            //notification.Channel.Should().Be(NotificationChannel.Courriel);
            //notification.Status.Should().Be(NotificationStatus.Pending);

           // notification.RetryCount.Should().Be(0);
        }

        [Fact]
        public void CreationNotificationSMS_DevraitCreerUneNotificationValide()
        {
            // Arrange
            string message = "Test message";
            var telephone = "418 200 1234";
            var typeDeNotification = NotificationType.LivreReserveDisponible;

            //// Act
            //var notification = Notification.CreationNotificationSms(message, typeDeNotification, telephone);

            //// Assert
            //notification.Message.Should().Be(message);
            //notification.Message.Should().NotBeNullOrWhiteSpace();
            //notification.Telephone.Should().Be(telephone);
            //notification.Telephone.Should().NotBeNullOrWhiteSpace();
            //notification.Type.Should().Be(typeDeNotification);
            //notification.Channel.Should().Be(NotificationChannel.SMS);
            //notification.Status.Should().Be(NotificationStatus.Pending);
            //notification.RetryCount.Should().Be(0);
        }

        [Fact]
        public void CreationNotificationInterne_DevraitCreerUneNotificationValide()
        {
            // Arrange
            string message = "Test message";
            string courriel = "test@example.com";
            var telephone = "418 200 1234";
            var typeDeNotification = NotificationType.StockBas;

            //// Act
            //var notification = Notification.CreationNotificationInterne(message, typeDeNotification, courriel,telephone);

            //// Assert
            //notification.Message.Should().Be(message);
            //notification.Message.Should().NotBeNullOrWhiteSpace();
            //notification.Courriel.Should().Be(courriel);
            //notification.Courriel.Should().NotBeNullOrWhiteSpace();
            //notification.Telephone.Should().Be(telephone);
            //notification.Telephone.Should().NotBeNullOrWhiteSpace();
            //notification.Type.Should().Be(typeDeNotification);
            //notification.Channel.Should().Be(NotificationChannel.Interne);
            //notification.Status.Should().Be(NotificationStatus.Pending);
            //notification.RetryCount.Should().Be(0);
        }

        [Fact]
        public void MarquerCommeEnvoyee_DevraitModifierLeStatut()
        {
            //// Arrange
            //string message = "Test message";
            //string courriel = "test@example.com";
            //var typeDeNotification = NotificationType.LivreReserveDisponible;        
            //var notification = Notification.CreationNotificationCourriel(message, typeDeNotification, courriel);

            //// Act
            //notification.MarquerCommeEnvoyee();

            //// Assert
            //notification.Status.Should().Be(NotificationStatus.Sent);
            //notification.SentAt.Should().NotBeNull();
            //notification.UpdatedAt.Should().NotBeNull();
        }


        [Fact]
        public void MarquerCommeEchouee_DevraitModifierLeStatutEtIncrementRetryCount()
        {
            //// Arrange
            //string message = "Test message";
            //string courriel = "test@example.com";
            //var typeDeNotification = NotificationType.LivreReserveDisponible;
            //var notification = Notification.CreationNotificationCourriel(message, typeDeNotification, courriel);
            //string errorMessage = "Error sending email";

            //// Act
            //notification.MarquerCommeEchouee(errorMessage);

            //// Assert
            //notification.Status.Should().Be(NotificationStatus.Failed);
            //notification.ErrorMessage.Should().Be(errorMessage);
            //notification.RetryCount.Should().Be(1);
            //notification.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void CanRetry_DevraitRetournerTrue_QuandRetryCountEstInferieurA3()
        {
            //// Arrange
            //string message = "Test message";
            //    string courriel = "test@example.com";
            //var typeDeNotification = NotificationType.LivreReserveDisponible;
            //var notification = Notification.CreationNotificationCourriel(message, typeDeNotification, courriel);
            //notification.MarquerCommeEchouee("Error");

            //// Act & Assert
            //notification.CanRetry().Should().BeTrue();

            //// Arrange - 2e tentative
            //notification.MarquerCommeEchouee("Error");

            //// Act & Assert
            //notification.CanRetry().Should().BeTrue();
        }

        [Fact]
        public void CanRetry_DevraitRetournerFalse_QuandRetryCountEst3()
        {
        //    // Arrange
        //    string message = "Test message";
        //    string courriel = "test@example.com";
        //    var typeDeNotification = NotificationType.LivreReserveDisponible;
        //    var notification = Notification.CreationNotificationCourriel(message, typeDeNotification, courriel);

        //    // 3 échecs consécutifs
        //    notification.MarquerCommeEchouee("Error 1");
        //    notification.MarquerCommeEchouee("Error 2");
        //    notification.MarquerCommeEchouee("Error 3");

        //    // Act & Assert
        //    notification.CanRetry().Should().BeFalse();
        }

        [Fact]
        public void CanRetry_DevraitRetournerFalse_QuandStatusEstSent()
        {
            //// Arrange
            //string message = "Test message";
            //string courriel = "test@example.com";
            //var typeDeNotification = NotificationType.LivreReserveDisponible;
            //var notification = Notification.CreationNotificationCourriel( message, typeDeNotification, courriel);
            //notification.MarquerCommeEnvoyee();

            //// Act & Assert
            //notification.CanRetry().Should().BeFalse();
        }
    }
}
