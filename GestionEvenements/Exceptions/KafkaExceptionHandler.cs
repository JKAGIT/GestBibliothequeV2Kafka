using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEvenements.Exceptions
{
    public static class KafkaExceptionHandler
    {
        public static void Handle(Exception ex)
        {
            // Logger ou autre traitement global ici
            Console.WriteLine($"Kafka Error: {ex.Message}");
        }
    }
}
