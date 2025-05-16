using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Retours
{
    public class AjouterRetourCommand : IRequest<Guid>
    {
        public Guid IDEmprunt { get; set; }
        public DateTime DateRetour { get; set; }

        public AjouterRetourCommand(Guid idEmprunt, DateTime dateRetour)
        {
            IDEmprunt = idEmprunt;
            DateRetour = dateRetour;
        }
    }
}
