using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ContactFormWithEmail.Models;

namespace ContactFormWithEmail.ViewModels
{


    public class MessageViewModel
    {

        public MessageViewModel(Message message)
        {
            Id = message.Id;
            SenderName = message.SenderName;
            Subject = message.Subject;
            SenderEmail = message.SenderEmail;
            RecipientEmail = message.RecipientEmail;
            Body = message.Body;
        }
        public int Id { get; set; }
        [Display(Name = "Your Name")]
        [Required(ErrorMessage = "Please enter your Name")]
        public string SenderName { get; set; }
        [Required(ErrorMessage = "Please enter an email subject")]
        public string Subject { get; set; }
        [Display(Name = "Your Email Address")]
        [EmailAddress]
        [Required(ErrorMessage = "Please enter your own email address")]
        public string SenderEmail { get; set; }
        [Display(Name = "Recipient's Email Address")]
        [EmailAddress]
        [Required(ErrorMessage = "Please enter the recipient's email address")]
        public string RecipientEmail { get; set; }
        [Display(Name = "Message Body (Limit 2000 character)")]
        [MaxLength(2000, ErrorMessage = "The message body is limited to 2000 characters.")]
        [Required(ErrorMessage = "You have not entered a message.")]
        [MinLength(10, ErrorMessage = "The message body must have at least 10 characters")]
        public string Body { get; set; }

        public bool Success { get; set; }
    }
}