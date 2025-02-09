using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CleanAcsService.Application.Interfaces;
using CleanAcsService.Application.DTOs;
using CleanAcsService.Domain.Entities;

namespace CleanAcsService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        /// <summary>
        /// Envia uma mensagem de chat para um thread especificado.
        /// </summary>
        /// <param name="request">Dados do chat (threadId, sender e conteúdo)</param>
        /// <returns>Status da operação</returns>
        [HttpPost("send")]
        public async Task<IActionResult> SendChatMessage([FromBody] SendChatMessageRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.ThreadId) ||
                string.IsNullOrEmpty(request.Sender) || string.IsNullOrEmpty(request.Content))
                return BadRequest("Invalid request payload.");

            var chatMessage = new ChatMessage(request.ThreadId, request.Sender, request.Content);
            try
            {
                await _chatService.SendChatMessageAsync(chatMessage);
                return Ok("Chat message sent successfully.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
