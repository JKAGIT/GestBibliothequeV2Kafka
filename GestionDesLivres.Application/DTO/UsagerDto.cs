namespace GestionDesLivres.Application.DTO
{
    public class UsagerDto
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenoms { get; set; } = string.Empty;
        public string Courriel { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public IEnumerable<EmpruntDto> Emprunts { get; set; } = new List<EmpruntDto>();
    }
}
