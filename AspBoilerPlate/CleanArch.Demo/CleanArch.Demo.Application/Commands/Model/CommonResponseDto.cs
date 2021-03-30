using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Commands.Model
{
   public class CommonResponseDto
    {
        public Guid ApplicationId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
