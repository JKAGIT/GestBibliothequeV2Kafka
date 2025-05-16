using GestionDesLivres.Domain.Entities;
using GestionDesLivres.Domain.Repositories;
using GestionDesLivres.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionDesLivres.Domain.Common.Interfaces;

namespace GestionDesLivres.Infrastructure.Repositories
{
    public class UsagerRepository : IUsagerRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsagerRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;              
        }

        public async Task AddAsync(Usager usager)
        {
            await _unitOfWork.Usagers.AddAsync(usager);
        }
        public async Task UpdateAsync(Usager usager)
        {
            await _unitOfWork.Usagers.UpdateAsync(usager);
        }
        public async Task DeleteAsync(Guid idUsager)
        {
            await _unitOfWork.Usagers.DeleteAsync(idUsager);
        }

        
        public async Task<IEnumerable<Usager>> GetAllAsync() 
        {

            return await _unitOfWork.Usagers.GetAllAsync();
        }

        public async Task<Usager> GetByIdAsync(Guid idUsager)
        {
            return await _unitOfWork.Usagers.GetByIdAsync(idUsager);
        }


    }
}
