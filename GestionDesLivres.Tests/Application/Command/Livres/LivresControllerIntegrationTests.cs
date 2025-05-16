using GestionDesLivres.Application.Commands.Livres;
using System.Net.Http.Json;
using System.Net;

public class LivresControllerIntegrationTests
{
    private readonly HttpClient _client;
    // Setup du client HTTP pour les tests

    [Fact]
    public async Task AjouterLivre_AvecDonneesValides_RetourneLivreId()
    {
        // Arrange
        var command = new AjouterLivreCommand(
            "Nouveau Titre",
            "Nouvel Auteur",
            Guid.Parse("une-categorie-id-qui-existe"),
            10);

        // Act
        var response = await _client.PostAsJsonAsync("/api/livres", command);

        // Assert
        response.EnsureSuccessStatusCode();
        var livreId = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, livreId);
    }

    [Fact]
    public async Task AjouterLivre_AvecDonneesInvalides_RetourneBadRequest()
    {
        // Arrange
        var commandInvalide = new AjouterLivreCommand(
            "", // Titre vide
            "Auteur",
            Guid.Parse("une-categorie-id-qui-existe"),
            10);

        // Act
        var response = await _client.PostAsJsonAsync("/api/livres", commandInvalide);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}