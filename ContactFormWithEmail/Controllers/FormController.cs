using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ContactFormWithEmail.Models;
using ContactFormWithEmail.ViewModels;
using ContactFormWithEmail.Services;
using Microsoft.AspNet.Identity;

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
            string currentUserId = User.Identity.GetUserId();
            IList<Message> messagesWhereUserIdMatches =
                _messageRepository.FindAny(u => u.UserId == currentUserId);
            if (messagesWhereUserIdMatches.Count == 0)
                return View("NoMessagesToDisplay");
            return View(messagesWhereUserIdMatches);
            
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
            //Get the current user Id
            string currentUserId = User.Identity.GetUserId();

            //If the ID is null, send user to register
            if (currentUserId == null)
                return RedirectToAction("Register", "Account");
            message.TimeStamp = DateTime.UtcNow;
            message.UserId = currentUserId;
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