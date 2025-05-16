using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    namespace GestBibliotheque.Utilitaires
    {
        public static class GestionErreurs
        {
            public static string GererErreur(Exception ex)
            {
                if (ex is ValidationException || ex is ArgumentNullException || ex is InvalidOperationException || ex is KeyNotFoundException)
                {
                    return ex.Message;
                }
                else
                {
                    return $"Une erreur inattendue est survenue. Détails : {ex.Message}";
                }
            }

        }
    }
}

