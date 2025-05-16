using AutoMapper;
using GestionDesLivres.Application.DTO;
using GestionDesLivres.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GestionDesLivres.Domain.Common;

namespace GestionDesLivres.Application.Mappings
{
    public class GestionDesLivresProfile : Profile
    {
        public GestionDesLivresProfile()
        {
            CreateMap<Livre, LivreDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.CategorieId, opt => opt.MapFrom(src => src.IDCategorie))
                .ForMember(dest => dest.Titre, opt => opt.MapFrom(src => src.Titre))
                .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
                .ForMember(dest => dest.Auteur, opt => opt.MapFrom(src => src.Auteur))
                .ForMember(dest => dest.Libelle, opt => opt.MapFrom(src => src.Categorie.Libelle));


            CreateMap<Categorie, CategorieDto>()
                   .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                   .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                   .ForMember(dest => dest.Libelle, opt => opt.MapFrom(src => src.Libelle));

            CreateMap<Usager, UsagerDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.Nom))
                .ForMember(dest => dest.Prenoms, opt => opt.MapFrom(src => src.Prenoms))
                .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone))
                .ForMember(dest => dest.Courriel, opt => opt.MapFrom(src => src.Courriel));

            CreateMap<Emprunt, EmpruntDto>()
               .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
               .ForMember(dest => dest.IDLivre, opt => opt.MapFrom(src => src.IDLivre))
               .ForMember(dest => dest.IDReservation, opt => opt.MapFrom(src => src.IDReservation))
               .ForMember(dest => dest.IDUsager, opt => opt.MapFrom(src => src.IDUsager))
               .ForMember(dest => dest.DateDebut, opt => opt.MapFrom(src => src.DateDebut))
               .ForMember(dest => dest.DateRetourPrevue, opt => opt.MapFrom(src => src.DateRetourPrevue))
               .ForMember(dest => dest.EstRendu, opt => opt.MapFrom(src => src.EstRendu))
               .ForMember(dest => dest.DateRetour, opt => opt.MapFrom(src => src.Retour.DateRetour))
               .ForMember(dest => dest.NomCompletUsager, opt => opt.MapFrom(src => src.Usager.Nom))
               .ForMember(dest => dest.TitreLivre, opt => opt.MapFrom(src => src.Livre.Titre));


            CreateMap<Retour, RetourDto>()
              .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
              .ForMember(dest => dest.IDEmprunt, opt => opt.MapFrom(src => src.IDEmprunt))
              .ForMember(dest => dest.DateRetour, opt => opt.MapFrom(src => src.DateRetour))
              .ForMember(dest => dest.Emprunt, opt => opt.MapFrom(src => src.Emprunt));

            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.IDLivre, opt => opt.MapFrom(src => src.IDLivre))
                .ForMember(dest => dest.IDUsager, opt => opt.MapFrom(src => src.IDUsager))
                .ForMember(dest => dest.DateDebut, opt => opt.MapFrom(src => src.DateDebut))
                .ForMember(dest => dest.DateRetourEstimee, opt => opt.MapFrom(src => src.DateRetourEstimee))
                .ForMember(dest => dest.Annuler, opt => opt.MapFrom(src => src.Annuler))
                .ForMember(dest => dest.NomCompletUsager, opt => opt.MapFrom(src => src.Usager.Nom))
                .ForMember(dest => dest.TitreLivre, opt => opt.MapFrom(src => src.Livre.Titre)) ;
        }
    }
}