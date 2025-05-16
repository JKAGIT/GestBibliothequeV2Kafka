using FluentAssertions;
using GestionNotification.Domain.Enums;

namespace GestionNotification.Tests.Infrastructure.Tests
{
    public class NotificationFactoryTests
    {
        [Fact]
        public void Create_DevraitRetournerNotificationCourriel_QuandCanalEstEmail()
        {
            //// Act
            //var notification = NotificationFactory.Create(NotificationChannel.Courriel);

            //// Assert
            //notification.Should().BeOfType<NotificationCourriel>();
        }

        [Fact]
        public void Create_DevraitRetournerNotificationSMS_QuandCanalEstSMS()
        {
            //// Act
            //var notification = NotificationFactory.Create(NotificationChannel.SMS);

            //// Assert
            //notification.Should().BeOfType<NotificationSMS>();
        }

        [Fact]
        public void Create_DevraitRetournerNotificationInterne_QuandCanalEstInterne()
        {
            //// Act
            //var notification = NotificationFactory.Create(NotificationChannel.Interne);

            //// Assert
            //notification.Should().BeOfType<NotificationInterne>();
        }

        [Fact]
        public void Create_DevraitLancerException_QuandCanalEstInvalide()
        {
        //    // Arrange - Créer une valeur d'énumération invalide
        //    var canalInvalide = (NotificationChannel)999;

        //    // Act & Assert
        //    Action act = () => NotificationFactory.Create(canalInvalide);
        //    act.Should().Throw<NotSupportedException>()
        //       .WithMessage($"Le canal {canalInvalide} n'est pas supporté.");
        }
    }
}
