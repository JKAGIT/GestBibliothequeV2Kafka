using GestionNotification.Infrastructure.Interfaces;
using Serilog;
using Microsoft.Extensions.Configuration;
using RestSharp;



namespace GestionNotification.Infrastructure.Services
{
    public class CourrielService : ICourrielService
    {
        private readonly string _apiKey;
        private readonly string _senderCourriel;
        private readonly bool _modeSimulation;

        public CourrielService(IConfiguration configuration, bool isTestMode)
        {
            _apiKey = configuration["Brevo:ApiKey"];
            _senderCourriel = configuration["Brevo:senderCourriel"] ;
            _modeSimulation = isTestMode;
        }

        public async Task EnvoyerCourrielAsync(string destinataire, string sujet, string message)
        {
            if (!_modeSimulation)
            {
                //PRODUCTION
                Log.Information("🔄 Systeme d'envoi reel sera implementé pour la prod. Courriel non envoyé à {Destinataire}. Message : {Message}", destinataire, message);
                return;
            }

            var client = new RestClient("https://api.brevo.com/v3/smtp/email");
            var request = new RestRequest();
            request.Method = Method.Post;

            request.AddHeader("accept", "application/json");
            request.AddHeader("api-key", _apiKey);
            request.AddHeader("content-type", "application/json");

            var body = new
            {
                sender = new { name = "Gestion Notification", email = _senderCourriel },
                to = new[] { new { email = destinataire, name = "Usager" } },
                subject = sujet,
                htmlContent = $"<strong>{message}</strong>"
            };

            request.AddJsonBody(body);

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Log.Information("📧 Email envoyé à {Destinataire}, Statut: {StatusCode}", destinataire, response.StatusCode);
            }
            else
            {
                Log.Error("❌ Échec de l'envoi de l'email à {Destinataire}. Code: {Code}, Message: {Message}", destinataire, response.StatusCode, response.Content);
            }
        }
    }
}


