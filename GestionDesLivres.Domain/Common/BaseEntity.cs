
namespace GestionDesLivres.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid ID { get; set; }
        public DateTime DateCreation { get; set; } = DateTime.UtcNow;

    }
}
