using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CleanAcsService.Application.Interfaces;
using CleanAcsService.Application.DTOs;
using CleanAcsService.Domain.Entities;

namespace CleanAcsService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        /// <summary>
        /// Envia um SMS utilizando o Azure Communication Services.
        /// </summary>
        /// <param name="request">Dados do SMS (destinatário e conteúdo)</param>
        /// <returns>Resultado do envio</returns>
        [HttpPost("send")]
        public async Task<IActionResult> SendSms([FromBody] SendSmsRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Destination) || string.IsNullOrEmpty(request.Content))
                return BadRequest("Invalid request payload.");

            var smsMessage = new SmsMessage(request.Destination, request.Content);
            try
            {
                await _smsService.SendSmsAsync(smsMessage);
                return Ok("SMS sent successfully.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
