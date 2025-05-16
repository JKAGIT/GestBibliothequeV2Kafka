using GestionDesLivres.Application.DTO;
using MediatR;

namespace GestionDesLivres.Application.Queries
{
    public class ObtenirTousUsagersQuery : IRequest<IEnumerable<UsagerDto>>
    {
    }
}
