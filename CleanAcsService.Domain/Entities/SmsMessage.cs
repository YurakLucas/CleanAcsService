using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAcsService.Domain.Entities
{
    public class SmsMessage
    {
        public string Destination { get; }
        public string Content { get; }

        public SmsMessage(string destination, string content)
        {
            Destination = destination;
            Content = content;
        }
    }
}

