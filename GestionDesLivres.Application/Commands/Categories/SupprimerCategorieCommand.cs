using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Categories
{
    public class SupprimerCategorieCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public SupprimerCategorieCommand(Guid id)
        {
            Id = id;
        }
    }
}
