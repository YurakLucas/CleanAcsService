using Azure;
using Azure.Communication.Email;
using CleanAcsService.Application.Interfaces;
using CleanAcsService.Domain.Entities;

namespace CleanAcsService.Infrastructure.Services
{
    public class AcsEmailService : IEmailService
    {
        private readonly string _connectionString;
        private readonly string _senderAddress;

        public AcsEmailService(string connectionString, string senderAddress)
        {
            _connectionString = connectionString;
            _senderAddress = senderAddress;
        }

        public async Task SendEmailAsync(EmailNotification message)
        {
            if (string.IsNullOrEmpty(_connectionString) || string.IsNullOrEmpty(_senderAddress))
                throw new InvalidOperationException("ACS email service not properly configured.");

            // Cria o cliente de email usando o connection string do ACS.
            var emailClient = new EmailClient(_connectionString);

            // Configura o conteúdo do email (texto simples e HTML).
            var emailContent = new EmailContent(message.Subject)
            {
                PlainText = message.Body,
                Html = $"<p>{message.Body}</p>"
            };

            // Define os destinatários.
            var recipients = new EmailRecipients(new[]
            {
                new EmailAddress(message.To)
            });

            // Cria a mensagem de email utilizando a classe do SDK do ACS Email.
            var emailMsg = new Azure.Communication.Email.EmailMessage(_senderAddress, recipients, emailContent);

            // Chama o método SendAsync utilizando a nova assinatura:
            // O primeiro argumento define se a operação deve aguardar a conclusão.
            var sendOperation = await emailClient.SendAsync(WaitUntil.Completed, emailMsg);

            // Recupera o resultado da operação.
            var result = sendOperation.Value;
            if (result.Status != EmailSendStatus.Succeeded)
            {
                throw new Exception("Failed to send email via ACS Email.");
            }
        }
    }
}