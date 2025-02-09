using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAcsService.Domain.Entities
{
    public class EmailNotification
    {
        public string To { get; }
        public string Subject { get; }
        public string Body { get; }

        public EmailNotification(string to, string subject, string body)
        {
            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
