using GestionEvenements.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEvenements.Events.Contracts
{
    public abstract class BaseEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public abstract NotificationType Type { get; }
    }

}
