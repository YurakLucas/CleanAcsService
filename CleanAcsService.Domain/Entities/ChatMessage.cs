using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAcsService.Domain.Entities
{
    public class ChatMessage
    {
        public string ThreadId { get; }
        public string Sender { get; }
        public string Content { get; }

        public ChatMessage(string threadId, string sender, string content)
        {
            ThreadId = threadId;
            Sender = sender;
            Content = content;
        }
    }
}
