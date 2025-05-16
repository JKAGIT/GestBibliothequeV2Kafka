using GestionDesLivres.Domain.Common;
using GestionDesLivres.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDesLivres.Application.DTO
{
    public class RetourDto
    {
        public Guid ID { get; set; }
        public Guid IDEmprunt { get; set; }

        public Emprunt? Emprunt { get; set; }

        public DateTime DateRetour { get; set; }
    }
}
