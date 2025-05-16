using GestionNotification.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionNotification.Domain.Enums
{
    public enum NotificationStatus
    {
        Attente,
        EnCours,
        Envoye,
        Echec,
        Annule
    }
}