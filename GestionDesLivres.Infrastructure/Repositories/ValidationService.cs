using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GestionDesLivres.Infrastructure.Repositories
{
    public static class ValidationService
    {
        public static void VerifierNull<T>(T valeur, string nomArgument, string paramArgument)
        {
            if (valeur == null)
            {
                throw new ArgumentNullException(nomArgument, string.Format("{ 0 } ne peut pas être nul.Assurez - vous que toutes les données sont correctement fournies.", paramArgument));
            }
        }

        public static void EnregistrementNonTrouve<T>(T entite, string nomEntite, Guid identifiant)
        {
            if (entite == null)
            {
                throw new KeyNotFoundException(string.Format("L'entité {0} avec l'ID {1} n'a pas été trouvée.", nomEntite, identifiant));
            }
        }

        public static void VerifierDate(DateTime? date, string typeDate)
        {
            if (date == null || date == default)
            {
                throw new KeyNotFoundException(string.Format("La date {0} doit être valide", typeDate));
            }

        }
    }
}
