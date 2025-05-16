using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.Commands.Livres
{
    public class MettreAJourStockLivreCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public int QuantiteAjoutee { get; set; }
        public MettreAJourStockLivreCommand(Guid id, int quantiteAjoutee)
        {
            Id = id;
            QuantiteAjoutee = quantiteAjoutee;
        }
    }
}
