using MediatR;

namespace GestionDesLivres.Application.Commands.Usagers
{
    public class SupprimerUsagerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public SupprimerUsagerCommand(Guid id)
        {
            Id = id;
        }
    }
}
