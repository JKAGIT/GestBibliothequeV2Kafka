using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEvenements.Enums
{
    public enum NotificationStatus
    {
        Pending,    
        Processing, // En cours de traitement
        Sent,      
        Failed,   
        Cancelled  
    }
}