using System.Threading.Tasks;
using CleanAcsService.Domain.Entities;

namespace CleanAcsService.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailNotification message);
    }
}