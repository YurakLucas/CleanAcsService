using System;
using System.Threading.Tasks;
using CleanAcsService.Application.Interfaces;
using CleanAcsService.Domain.Entities;
using Azure.Communication.Sms;
using System.Net.Mail;

namespace CleanAcsService.Infrastructure.Services
{
    public class AcsSmsService : ISmsService
    {
        private readonly string _connectionString;
        private readonly string _fromPhoneNumber;

        public AcsSmsService(string connectionString, string fromPhoneNumber)
        {
            _connectionString = connectionString;
            _fromPhoneNumber = fromPhoneNumber;
        }

        public async Task SendSmsAsync(SmsMessage message)
        {
            if (string.IsNullOrEmpty(_connectionString) || string.IsNullOrEmpty(_fromPhoneNumber))
                throw new InvalidOperationException("ACS connection string or from phone number is not configured.");

            // Cria o cliente do ACS para SMS
            var smsClient = new SmsClient(_connectionString);

            // Envia o SMS
            var response = await smsClient.SendAsync(
                from: _fromPhoneNumber,
                to: message.Destination,
                message: message.Content);

            // Verifica se o envio foi bem-sucedido
            if (response.Value?.Successful != true)
            {
                throw new Exception("Failed to send SMS via ACS.");
            }
        }
    }
}
