using Microsoft.AspNetCore.Mvc;
using Notification.Application.Interfaces;
using Notification.Contracts.Email.Requests;

namespace Notification.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestEmailController : ControllerBase
    {
        private readonly INotificationService<SendEmailRequest> notificationService;


        public TestEmailController(
            INotificationService<SendEmailRequest> notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Test(string consumer, string message, string subject)
        {
            await notificationService.SendNotification(new SendEmailRequest 
            { 
                Consumer = consumer,
                Message = message,
                Subject = subject
            });

            return Ok();
        }
    }
}
