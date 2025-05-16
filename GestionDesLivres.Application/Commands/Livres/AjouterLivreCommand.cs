using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionDesLivres.Domain.Entities;
using MediatR;

namespace GestionDesLivres.Application.Commands.Livres
{
    public class AjouterLivreCommand:IRequest<Guid>
    {
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public Guid CategorieId { get; set; }
        public int Stock { get; set; }

        public AjouterLivreCommand(string titre, string auteur, Guid categorieId, int stock)
        {
            Titre = titre;
            Auteur = auteur;
            CategorieId = categorieId;
            Stock = stock;
        }
    }
}