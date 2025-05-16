using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Emprunts
{
    public class SupprimerEmpruntCommand : IRequest<bool>
    {
        public Guid ID { get; set; }

        public SupprimerEmpruntCommand(Guid id)
        {
            ID = id;
        }
    }
}
