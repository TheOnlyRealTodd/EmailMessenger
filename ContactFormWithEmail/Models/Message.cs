using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactFormWithEmail.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
        [DomainsNotAllowed]
        public string SenderEmail { get; set; }
        public string RecipientEmail { get; set; }
        [MaxLength(2000)]
        public string Body { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}