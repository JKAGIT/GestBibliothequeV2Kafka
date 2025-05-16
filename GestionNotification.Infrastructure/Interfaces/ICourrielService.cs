using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionNotification.Infrastructure.Interfaces
{
    public interface ICourrielService
    {
        Task EnvoyerCourrielAsync(string destinataire, string sujet, string corps);
    }
}
