
using GestionDesLivres.Application.Services;
using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Domain.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GestionDesLivres.Application.Commands.Emprunts
{
    public class AjouterEmpruntCommandHandler : IRequestHandler<AjouterEmpruntCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILivreRepository _livreRepository;
        private readonly LivreService _livreService;
        private readonly StockService _stockService;

        private readonly IRecherche<Emprunt> _recherche;

        public AjouterEmpruntCommandHandler(IUnitOfWork unitOfWork, ILivreRepository livreRepository, LivreService livreService, IRecherche<Emprunt> recherche, StockService stockService)
        {
            _unitOfWork = unitOfWork;
            _livreRepository = livreRepository;
            _livreService = livreService;
            _recherche = recherche;
            _stockService = stockService;
        }

        public async Task<Guid> Handle(AjouterEmpruntCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<string> validationErrors = new List<string>();
                if (request.IDLivre == Guid.Empty || request.IDLivre == Guid.Empty)
                    validationErrors.Add(ErreurMessageProvider.GetMessage("ValeurNulle"));

                var emprunt = new Emprunt
                {
                    ID = Guid.NewGuid(),
                    IDUsager = request.IDUsager,
                    IDLivre = request.IDLivre,
                    DateDebut = request.DateDebut,
                    DateRetourPrevue = request.DateRetourPrevue,
                    IDReservation = request.IDReservation
                };

                await _unitOfWork.Emprunts.AddAsync(emprunt);
                await _livreRepository.MettreAJourStock(emprunt.IDLivre, -1);
                await _unitOfWork.CompleteAsync();

                // Appel du service Kafka ici -- Notif usager et notif utilisateur en cas de stock bas
                #region Preparation pour constituer les messages KAFKA 

                var infoLivreUsagerPourKafka = await _recherche.GetAll()
                                            .Include(e => e.Livre)
                                            .Include(e => e.Usager)
                                            .Where(e => e.IDUsager == request.IDUsager &&
                                                        e.IDLivre == request.IDLivre &&
                                                        !e.EstRendu)
                                            .OrderByDescending(e => e.DateCreation)
                                            .FirstOrDefaultAsync();

                if (infoLivreUsagerPourKafka == null)
                    throw new ValidationException(ErreurMessageProvider.GetMessage("EnregistrementNonTrouve", "Livre", request.IDLivre));

                var titre = $"{infoLivreUsagerPourKafka.Livre?.Titre} de {infoLivreUsagerPourKafka.Livre?.Auteur}";

                if (infoLivreUsagerPourKafka.Livre?.Stock <= 5)
                    await _stockService.StockBas(request.IDLivre, titre, infoLivreUsagerPourKafka.Livre.Stock);


                await _livreService.EmprunterLivre(request.IDLivre, request.IDUsager, titre, infoLivreUsagerPourKafka.Usager.Courriel, infoLivreUsagerPourKafka.Usager.Telephone);               

                #endregion

                return emprunt.ID;
            }
            catch (ValidationException ex)
            {
                throw new ValidationException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur inattendue s'est produite.", ex);
            }
        }
    }
}
