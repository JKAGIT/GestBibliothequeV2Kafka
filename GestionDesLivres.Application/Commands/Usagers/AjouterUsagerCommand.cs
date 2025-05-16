using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionDesLivres.Domain.Entities;
using MediatR;

namespace GestionDesLivres.Application.Commands.Usagers
{
    public class AjouterUsagerCommand : IRequest<Guid>
    {
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public string Courriel { get; set; }
        public string Telephone { get; set; }
        public AjouterUsagerCommand(string nom,string prenoms,string courriel,string telephone)
        {
            Nom = nom;
            Prenoms = prenoms;
            Courriel=courriel;
            Telephone = telephone;
        }
    }

}
