using Xunit;
using Moq;
using GestionEvenements.Configuration;
using GestionEvenements.Serialization;
using GestionEvenements.Events;
using GestionEvenements.Enums;

namespace GestionEvenement.Tests
{
    //public class KafkaServiceTests
    //{
    //    [Fact]
    //    public async Task PublierAsync_Envoie_Message_Correct()
    //    {
    //        var config = new KafkaConfig(); // Mock config
    //        var serializer = new JsonSerializer(); // Mock serializer
    //        var kafkaService = new KafkaService(config);

    //        var testMessage = new LivreEventDto
    //        {
    //            LivreId = Guid.NewGuid(),
    //            TypeNotification = NotificationType.LivreEmprunte,
    //            DateEvenement = DateTime.UtcNow
    //        };

    //        await kafkaService.EnvoyerMessageAsync("TestTopic", testMessage);

    //        Assert.NotNull(testMessage);
    //    }
    //    [Fact]
    //    public async Task EcouterMessageAsync_Traite_Message_Correctement()
    //    {
    //        var config = new KafkaConfig(); // Mock config
    //        var kafkaService = new KafkaService(config);

    //        string receivedMessage = null;
    //        async Task Handler(string message) => receivedMessage = message;

    //        await kafkaService.EcouterMessageAsync("TestTopic", Handler);

    //        Assert.NotNull(receivedMessage);
    //    }
    //}

    public class KafkaServiceTests
    {
        //[Fact]
        //public async Task PublierAsync_Envoie_Message_Correct()
        //{
        //    // Arrange
        //    var config = new KafkaConfig
        //    {
        //        BootstrapServers = "localhost:9092"
        //    };
        //    var kafkaService = new KafkaService(config);

        //    var testMessage = new LivreEvent
        //    {
        //        LivreId = Guid.NewGuid(),
        //        TypeNotification = NotificationType.LivreEmprunte,
        //        DateEvenement = DateTime.UtcNow
        //    };

        //    // Act
        //    var exception = await Record.ExceptionAsync(() =>
        //        kafkaService.EnvoyerMessageAsync("NotificationEnvoyee", testMessage));

        //    // Assert
        //    Assert.Null(exception); // pas d'erreur = le message est bien passé par le pipeline
        //}

        //[Fact]
        //public async Task EcouterMessageAsync_ReceivesMessage_WhenMessageIsSent()
        //{
        //    // Arrange
        //    var config = new KafkaConfig { BootstrapServers = "localhost:9092" };
        //    var service = new KafkaService(config);

        //    string receivedMessage = null;
        //    using var cts = new CancellationTokenSource();
        //    async Task Handler(string message)
        //    {
        //        receivedMessage = message;
        //        cts.Cancel(); 
        //        await Task.CompletedTask;
        //    }

        //    // Mock message reception
        //    await service.EcouterMessageAsync("TestTopic", Handler, cts.Token);

        //    // Assert
        //    Assert.NotNull(receivedMessage);
        //    Assert.Equal("Message de test", receivedMessage);
        //}

        //[Fact]
        //public async Task EcouterMessageAsync_ThrowsException_WhenTopicIsEmpty()
        //{
        //    // Arrange
        //    var config = new KafkaConfig { BootstrapServers = "localhost:9092" };
        //    var service = new KafkaService(config);
        //    using var cts = new CancellationTokenSource();

        //    // Act & Assert
        //    var exception = await Assert.ThrowsAsync<ArgumentException>(() => service.EcouterMessageAsync("", message => Task.CompletedTask, cts.Token));
        //    Assert.Equal("Le nom du topic ne peut pas être null ou vide.", exception.Message);
        //}

    }

}





