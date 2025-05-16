using MediatR;
namespace GestionDesLivres.Application.Commands.Emprunts
{
    public class AjouterEmpruntCommand : IRequest<Guid>
    {
        public Guid IDLivre { get; set; }
        public Guid IDUsager { get; set; }
        public DateTime DateRetourPrevue { get; set; }
        public DateTime DateDebut { get; set; }
        public Guid? IDReservation { get; set; } // en cas d'un emprunt issu d'une reservation

        public AjouterEmpruntCommand(Guid idLivre, Guid idUsager, DateTime dateDebut, DateTime dateRetourPrevue, Guid? idReservation = null)
        {
            IDLivre = idLivre;
            IDUsager = idUsager;
            DateDebut = dateDebut;
            DateRetourPrevue = dateRetourPrevue;
            IDReservation = idReservation;
        }
    }
}
