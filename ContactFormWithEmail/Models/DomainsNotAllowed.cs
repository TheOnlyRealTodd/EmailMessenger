using System.ComponentModel.DataAnnotations;

namespace ContactFormWithEmail.Models
{
    public class DomainsNotAllowed : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var message = (Models.Message) validationContext.ObjectInstance;
            var senderEmail = message.SenderEmail.ToLower();
            if (senderEmail.Contains("@yahoo.com"))
            {
                return new ValidationResult("Yahoo email is not allowed. Please choose another email.");
            }
            return ValidationResult.Success;



        }
    }
}