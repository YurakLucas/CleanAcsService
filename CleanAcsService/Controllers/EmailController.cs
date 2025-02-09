using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CleanAcsService.Application.Interfaces;
using CleanAcsService.Application.DTOs;
using CleanAcsService.Domain.Entities;

namespace CleanAcsService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Envia um email utilizando o Azure Communication Services.
        /// </summary>
        /// <param name="request">Dados do email (destinatário, assunto e corpo)</param>
        /// <returns>Status do envio</returns>
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.To) ||
                string.IsNullOrEmpty(request.Subject) || string.IsNullOrEmpty(request.Body))
                return BadRequest("Invalid request payload.");

            var emailNotification = new EmailNotification(request.To, request.Subject, request.Body);
            try
            {
                await _emailService.SendEmailAsync(emailNotification);
                return Ok("Email sent successfully.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
