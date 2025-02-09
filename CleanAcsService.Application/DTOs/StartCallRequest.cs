using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAcsService.Application.DTOs
{
    public class StartCallRequest
    {
        public string Source { get; set; }

        public string Target { get; set; }
        public string CallbackUrl { get; set; }
    }
}
