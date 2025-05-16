using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Livres
{
    public class SupprimerLivreCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public SupprimerLivreCommand(Guid id)
        {
            Id = id;
        }
    }
}
