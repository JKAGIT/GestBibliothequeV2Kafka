using GestionDesLivres.Domain.Common.Interfaces;
using GestionDesLivres.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Domain.Repositories
{
    public interface ICategorieRepository : IGenericRepository<Categorie>
    {
        Task<Categorie> ObtenirCategorieParCode(string code);
    }
}
