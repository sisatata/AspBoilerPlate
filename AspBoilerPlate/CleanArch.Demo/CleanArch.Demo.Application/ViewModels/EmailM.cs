using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.ViewModels
{
   public class EmailModel
    {
        public List<string> ReceiverMailIds { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool AllowHtml { get; set; }
        public List<string> Attachments { get; set; }
    }
}
