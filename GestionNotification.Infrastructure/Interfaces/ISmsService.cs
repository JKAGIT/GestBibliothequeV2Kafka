using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionNotification.Infrastructure.Interfaces
{
    public interface ISmsService
    {
        Task EnvoyerSmsAsync(string numeroDestinataire, string message);
    }
}
