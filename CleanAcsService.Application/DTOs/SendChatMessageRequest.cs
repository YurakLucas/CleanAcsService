using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAcsService.Application.DTOs
{
    public class SendChatMessageRequest
    {
        public string ThreadId { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
    }
}