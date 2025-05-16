using GestionNotification.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace GestionNotification.Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _twilioNumber;
        private readonly string _receiveNumber;
        private readonly bool _modeSimulation;

        public SmsService(IConfiguration configuration, bool isTestMode)
        {
            _accountSid = configuration["Twilio:AccountSid"] ?? throw new ArgumentNullException(nameof(configuration), "Twilio:AccountSid is not configured.");
            _authToken = configuration["Twilio:AuthToken"] ?? throw new ArgumentNullException(nameof(configuration), "Twilio:AuthToken is not configured.");
            _twilioNumber = configuration["Twilio:PhoneNumber"] ?? throw new ArgumentNullException(nameof(configuration), "Twilio:PhoneNumber is not configured.");
            _receiveNumber = configuration["Twilio:ReceiveNumber"] ?? throw new ArgumentNullException(nameof(configuration), "Twilio:ReceiveNumber is not configured.");
            _modeSimulation = isTestMode;
        }

        public async Task EnvoyerSmsAsync(string numeroDestinataire, string message)
        {
            if (numeroDestinataire == _twilioNumber)
            {
                Log.Error("❌ Erreur : Le numéro de destination est identique au numéro Twilio !");
                return;
            }
            if (!_modeSimulation)
            {
                //PRODUCTION
                Log.Information("🔄 Systeme d'envoi reel sera implementé pour la prod. SMS non envoyé à {Destinataire}. Message : {Message}", numeroDestinataire, message);
                return;
            }
            TwilioClient.Init(_accountSid, _authToken);

            var response = await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber(_twilioNumber),
                to: new Twilio.Types.PhoneNumber(_receiveNumber)
            );

            Log.Information("📱 SMS envoyé à {Destinataire}", numeroDestinataire);
        }
    }
}
