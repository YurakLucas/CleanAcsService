using System.Threading.Tasks;
using CleanAcsService.Domain.Entities;

namespace CleanAcsService.Application.Interfaces
{
    public interface IChatService
    {
        Task SendChatMessageAsync(ChatMessage message);
    }
}
