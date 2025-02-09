using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAcsService.Application.DTOs
{
    public class SendSmsRequest
    {
        public string Destination { get; set; }
        public string Content { get; set; }
    }
}

