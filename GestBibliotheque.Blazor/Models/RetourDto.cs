namespace GestBibliotheque.Blazor.Models
{
    public class RetourDto
    {
        public Guid ID { get; set; }
        public Guid IDEmprunt { get; set; }

        public EmpruntDto? Emprunt { get; set; }

        public DateTime DateRetour { get; set; }
    }
}
