using System.Threading.Tasks;
using CleanAcsService.Domain.Entities;

namespace CleanAcsService.Application.Interfaces
{
    public interface ISmsService
    {
        Task SendSmsAsync(SmsMessage message);
    }
}
