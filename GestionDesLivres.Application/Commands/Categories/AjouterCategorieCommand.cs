using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Categories
{
    public class AjouterCategorieCommand : IRequest<Guid>
    {
        public string Code { get; set; }        
        public string Libelle { get; set; }
       

        public AjouterCategorieCommand(string libelle, string code)
        {
            Libelle = libelle;
            Code = code;
        }
    }
}
