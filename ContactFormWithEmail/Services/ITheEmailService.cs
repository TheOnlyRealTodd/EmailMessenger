using System.Threading.Tasks;
using ContactFormWithEmail.Models;

namespace ContactFormWithEmail.Services
{
    public interface ITheEmailService
    {
        Task SendAsync(Message message);
        Task configSendGridasync(Message ingestedMessage);
    }
}
