using System;
using System.Collections.Generic;

namespace Core.Domain.Model.MessageBroker
{
    public class EmailMessageModel 
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}