using System;
using System.Threading.Tasks;
using Azure.Communication.Chat;
using Azure.Communication;
using CleanAcsService.Application.Interfaces;
using CleanAcsService.Domain.Entities;

namespace CleanAcsService.Infrastructure.Services
{
    public class AcsChatService : IChatService
    {
        private readonly Uri _chatEndpoint;
        private readonly CommunicationTokenCredential _tokenCredential;

        public AcsChatService(string chatEndpoint, string chatToken)
        {
            if (string.IsNullOrWhiteSpace(chatEndpoint))
                throw new ArgumentException("Chat endpoint must be provided", nameof(chatEndpoint));
            if (string.IsNullOrWhiteSpace(chatToken))
                throw new ArgumentException("Chat token must be provided", nameof(chatToken));

            _chatEndpoint = new Uri(chatEndpoint);
            _tokenCredential = new CommunicationTokenCredential(chatToken);
        }

        public async Task SendChatMessageAsync(Domain.Entities.ChatMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            // Cria o ChatClient utilizando o endpoint e o token
            var chatClient = new ChatClient(_chatEndpoint, _tokenCredential);

            // Obtém o ChatThreadClient para o thread especificado
            var chatThreadClient = chatClient.GetChatThreadClient(message.ThreadId);

            // Envia a mensagem para o chat utilizando ChatMessageType.Text
            var response = await chatThreadClient.SendMessageAsync(message.Content, ChatMessageType.Text);

            if (response == null)
            {
                throw new Exception("Failed to send chat message via ACS.");
            }
        }
    }
}
