namespace GestBibliotheque.Blazor.Models
{
    public class ReservationDto
    {
        public Guid ID { get; set; }
        public Guid IDUsager { get; set; }
        public Guid IDLivre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateRetourEstimee { get; set; }
        public bool Annuler { get; set; }
        public string NomCompletUsager { get; set; }
        public string TitreLivre { get; set; }
        public string LivreDisponible { get; set; } // "OUI" / "NON"      
        public string StatutReservation => Annuler ? "Annulée" : (DateRetourEstimee < DateTime.Now ? "En retard" : "À jour");
        public bool EstEmprunte { get; set; }

    }
}
