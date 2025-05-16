using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Emprunts
{
    public class ModifierEmpruntCommand : IRequest<bool>
    {
        public Guid ID { get; set; }
        public DateTime DateRetourPrevue { get; set; }
        public DateTime DateDebut { get; set; }

        public ModifierEmpruntCommand(Guid id, DateTime dateRetourPrevue, DateTime dateDebut)
        {
            ID = id;
            DateDebut = dateDebut; 
            DateRetourPrevue = dateRetourPrevue;
        }
    }
}
