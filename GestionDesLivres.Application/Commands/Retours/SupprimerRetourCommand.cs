using MediatR;
using System;

namespace GestionDesLivres.Application.Retours.Commands
{
    public class SupprimerRetourCommand : IRequest<bool>
    {
        public Guid ID { get; set; }

        public SupprimerRetourCommand(Guid id)
        {
            ID = id;
        }
    }
}
