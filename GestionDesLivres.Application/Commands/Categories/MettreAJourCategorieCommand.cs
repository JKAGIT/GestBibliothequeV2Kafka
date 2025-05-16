using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Categories
{
    public class MettreAJourCategorieCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }
        public MettreAJourCategorieCommand(Guid id, string code, string libelle)
        {
            Id = id;
            Code = code;
            Libelle = libelle;
        }
    }
}
