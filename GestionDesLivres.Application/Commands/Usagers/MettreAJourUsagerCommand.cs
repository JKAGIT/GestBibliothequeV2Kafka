using MediatR;

namespace GestionDesLivres.Application.Commands.Usagers
{
    public class MettreAJourUsagerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public string Courriel { get; set; }
        public string Telephone { get; set; }
        public MettreAJourUsagerCommand(Guid id, string nom, string prenoms, string courriel, string telephone)
        {
            Id = id;
            Nom = nom;
            Prenoms = prenoms;
            Courriel = courriel;
            Telephone = telephone;
        }
    }
}
