using System.Threading.Tasks;
using ContactFormWithEmail.Models;
using SendGrid;
using System.Configuration;
using System.Diagnostics;

namespace ContactFormWithEmail.Services
{
    public class TheEmailService : ITheEmailService
    {

        public async Task SendAsync(Message message)
        {
            await configSendGridasync(message);
        }


        public async Task configSendGridasync(Message ingestedMessage)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(ingestedMessage.RecipientEmail);
            myMessage.From = new System.Net.Mail.MailAddress(
                                ingestedMessage.SenderEmail, ingestedMessage.SenderName);
            myMessage.Subject = ingestedMessage.Subject;
            myMessage.Text = ingestedMessage.Body;
            myMessage.Html = ingestedMessage.Body;

            string apiKey = ConfigurationManager.AppSettings["SendGridApiKey"];

            // Create a Web transport for sending email.
            var transportWeb = new Web(apiKey);

            // Send the email.
            if (transportWeb != null)
            {
                await transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
        }
    }
}