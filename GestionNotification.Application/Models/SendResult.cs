using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionNotification.Application.Models
{
    public class SendResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        public static SendResult Ok() => new SendResult { Success = true };
        public static SendResult Fail(string message) => new SendResult { Success = false, ErrorMessage = message };
    }
}

