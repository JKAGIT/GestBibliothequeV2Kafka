using GestionEvenements.Enums;
using GestionEvenements.Events.Contracts;

namespace GestionEvenements.Events
{
    public class StockEvent : BaseEvent
    {
        public override NotificationType Type { get; } = NotificationType.StockBas;
        public Guid LivreId { get; set; }
        public string Titre { get; set; }
        public int QuantiteRestante { get; set; }
       
        public StockEvent() { }
        public StockEvent(Guid livreId, string titre,int quantiteRestante)
        {
            LivreId = livreId != Guid.Empty ? livreId : throw new ArgumentException("ProduitId invalide");
            Titre = !string.IsNullOrWhiteSpace(titre) ? titre : throw new ArgumentException("Titre invalide");
            QuantiteRestante = quantiteRestante >= 0 ? quantiteRestante : throw new ArgumentException("Quantité invalide");
        }
        
    }
}
