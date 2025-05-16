using MediatR;
using System;

namespace GestionDesLivres.Application.Retours.Commands
{
    public class ModifierRetourCommand : IRequest<bool>
    {
        public Guid ID { get; set; }
        public DateTime DateRetour { get; set; }

        public ModifierRetourCommand(Guid id, DateTime dateRetour)
        {
            ID = id;
            DateRetour = dateRetour;
        }
    }
}