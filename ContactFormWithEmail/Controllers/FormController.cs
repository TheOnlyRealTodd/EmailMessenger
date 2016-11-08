using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ContactFormWithEmail.Models;
using ContactFormWithEmail.ViewModels;
using ContactFormWithEmail.Services;

namespace ContactFormWithEmail.Controllers
{
    [Authorize]
    public class FormController : Controller
    {
        private readonly IRepository<Message> _messageRepository;
        private ITheEmailService _sendGrid;
        public FormController(IRepository<Message> messageRepository, ITheEmailService sendGrid)
        {
            _messageRepository = messageRepository;
            _sendGrid = sendGrid;
        }
        //HTTP GET: Form
        [HttpGet]

        public ViewResult Index()
        {
            MessageViewModel viewModel = new MessageViewModel(new Message());
            viewModel.Success = false;
            return View("index",viewModel);
        }
        [HttpGet]

        public ViewResult Emails()
        {
            IList<Message> allMessagesInDb = _messageRepository.GetAll();

            return View(allMessagesInDb);
            
        }


        //HTTP POST: Form - [Bind] added to parameter to prevent the route from adding a null Id to the model
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save([Bind(Exclude = "Id")] Message message)
        {
             
            if (!ModelState.IsValid || message == null)
            {
                var viewModel = new MessageViewModel(new Message());
                return View("index",viewModel);
            }

            message.TimeStamp = DateTime.UtcNow;
            _messageRepository.Add(message);
            await SendEmail(message);
            var viewModelNew = new MessageViewModel(new Message()) {Success = true};
            return View("index",viewModelNew);

        }

        public async Task SendEmail(Message message)
        {
           await _sendGrid.SendAsync(message);
            
        }
    }
}