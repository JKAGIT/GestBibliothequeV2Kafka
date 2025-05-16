using GestionDesLivres.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Queries.Emprunts
{
    public class ObtenirEmpruntsParIdQuery : IRequest<EmpruntDto>
    {
        public Guid Id { get; set; }

        public ObtenirEmpruntsParIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
