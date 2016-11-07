using System;
using System.ComponentModel.DataAnnotations;

namespace ContactFormWithEmail.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string Subject { get; set; }
        [DomainsNotAllowed]
        [EmailAddress]
        public string SenderEmail { get; set; }
        [EmailAddress]
        public string RecipientEmail { get; set; }
        [MaxLength(2000)]
        public string Body { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}