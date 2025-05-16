//using GestionNotification.Application.Factories;
using GestionNotification.Application.Interfaces;
using GestionNotification.Application.Services;
using GestionNotification.Domain.Entities;
using GestionNotification.Domain.Repositories;
using Moq;
using GestionNotification.Domain.Enums;
using System.Linq.Expressions;

namespace GestionNotification.Test
{
    public class NotificationServiceTests
    {
        private readonly Mock<INotificationService> _mockService;
        private readonly Mock<INotificationRepository> _repoMock;
        private readonly INotificationService _service;
        private readonly INotificationSenderService _serviceSender;

        public NotificationServiceTests()
        {
            _mockService = new Mock<INotificationService>();

            _repoMock = new Mock<INotificationRepository>();
            _service = new NotificationService(_repoMock.Object, _serviceSender);
        }

        [Fact]
        //Tester si le service retourne bien une notification existante. → Vérifier que GetByIdAsync() retourne une notification valide si elle existe.
        //Utilisation de Mock<INotificationService> → Permet de simuler un retour sans dépendre de la base de données.
        public async Task GetByIdAsync_ReturnsNotification_WhenExists()
        {
            var notificationId = Guid.NewGuid();
           // var expectedNotification = new Notification("Message Test", NotificationType.LivreEmprunte, NotificationCanal.Courriel, Guid.NewGuid(), "jk@test.com", "4182552365", false);
            var expectedNotification = new Notification();

            _mockService.Setup(service => service.GetByIdAsync(notificationId))
                        .ReturnsAsync(expectedNotification);

            var result = await _mockService.Object.GetByIdAsync(notificationId);

            Assert.NotNull(result);
            Assert.Equal(expectedNotification.ID, result.ID);
            Assert.Equal("Message Test", result.Message);
        }


        [Fact]
        //Vérifier que AddAsync dans le service appelle bien le repository.
        public async Task AddAsync_Should_Call_Repository()
        {
            var expectedNotification = new Notification("Message Test", NotificationType.LivreEmprunte, NotificationCanal.Courriel, Guid.NewGuid(), "jk@test.com", "4182552365", false);
            //var expectedNotification = new Notification();

            await _service.AddAsync(expectedNotification);

            _repoMock.Verify(r => r.AddAsync(expectedNotification), Times.Once);
        }

        [Fact]
        //Tester que GetAllAsync retourne bien les données du repository.
        public async Task GetAllAsync_Should_Return_Notifications()
        {
            var data = new List<Notification> { new Notification("Message Test", NotificationType.LivreEmprunte, NotificationCanal.SMS, Guid.NewGuid(), "jk@test.com", "4182552365", false) };
            //var data = new List<Notification> { new Notification() };
            _repoMock.Setup(r => r.GetAllAsync(null)).ReturnsAsync(data);

            var result = await _service.GetAllAsync();

            Assert.Single(result);
        }

        [Fact]
        //Vérifier que la méthode SendNotificationAsync : marque la notification comme envoyée et appelle le repository pour la mise à jour
        public async Task SendNotificationAsync_Should_Mark_As_Sent_When_Success()
        {
            var notification = new Notification("Message Test", NotificationType.LivreEmprunte, NotificationCanal.Interne, Guid.NewGuid(), "jk@test.com", "4182552365", false);
            //var notification = new Notification();

          //  await _service.SendNotificationAsync(notification);
            Assert.Equal(NotificationStatus.Envoye, notification.Status);
            _repoMock.Verify(r => r.UpdateAsync(notification), Times.Once);
        }

        [Fact]
        public async Task RetryFailedNotificationsAsync_RetriesFailedNotifications()
        {
            var failedNotification = new Notification { ID = Guid.NewGuid(), Status = NotificationStatus.Echec, RetryCount = 1 };

            var mockService = new Mock<INotificationService>();
            mockService.Setup(service => service.GetByFilterAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                       .ReturnsAsync(new List<Notification> { failedNotification });

            await mockService.Object.RetryFailedNotificationsAsync();

            mockService.Verify(service => service.RetryFailedNotificationsAsync(), Times.Once);
        }

    }
}
