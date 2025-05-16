using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Livres
{
    public class MettreAJourLivreCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public Guid CategorieId { get; set; }
        public int Stock { get; set; }

        public MettreAJourLivreCommand(Guid id, string titre, string auteur, Guid categorieId, int stock)
        {
            Id = id;
            Titre = titre;
            Auteur = auteur;
            CategorieId = categorieId;
            Stock = stock;
        }
    }
}
