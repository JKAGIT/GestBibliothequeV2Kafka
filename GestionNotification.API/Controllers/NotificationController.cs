using GestionNotification.Application.Interfaces;
using GestionNotification.Domain.Entities;
using GestionNotification.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace GestionNotification.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
     public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: api/notifications
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationService.GetAllAsync();
            return Ok(notifications);
        }

        // GET: api/notifications/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(Guid id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null)
            {
              Log.Warning($"Notification ID={id} introuvable.");
                return NotFound();
            }

            return Ok(notification);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] Notification notification)
        {
            if (notification == null || string.IsNullOrWhiteSpace(notification.Message))
            {
                return BadRequest("Le champ 'Message' est obligatoire et ne peut être vide.");
            }

            // 🔹 Vérifier que `type` est bien une valeur valide de `NotificationType`
            if (!Enum.IsDefined(typeof(NotificationType), notification.Type))
            {
                return BadRequest($"Type invalide : {notification.Type}. Valeurs possibles : {string.Join(", ", Enum.GetNames(typeof(NotificationType)))}");
            }

            if (!Enum.IsDefined(typeof(NotificationCanal), notification.Canal))
            {
                return BadRequest($"Canal invalide : {notification.Canal}. Valeurs possibles : {string.Join(", ", Enum.GetNames(typeof(NotificationCanal)))}");
            }

            await _notificationService.AddAsync(notification);
            return CreatedAtAction(nameof(GetNotificationById), new { id = notification.ID }, notification);
        }        
    }
}

