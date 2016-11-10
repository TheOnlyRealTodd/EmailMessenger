using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ContactFormWithEmail.Controllers;
using ContactFormWithEmail.Models;
using ContactFormWithEmail.Services;
using ContactFormWithEmail.ViewModels;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace ContactFormWithEmail.Tests.Controllers
{
    [TestFixture]
    class FormControllerTests
    {
        private readonly Mock<IRepository<Message>> _mockRepository;
        private readonly Mock<ITheEmailService> _mockTheEmailService;
        private readonly FormController _controller;
        public FormControllerTests()
        {
            //Arrange
            _mockRepository = new Mock<IRepository<Message>>();
            _mockTheEmailService = new Mock<ITheEmailService>();
             _controller = new FormController(_mockRepository.Object, _mockTheEmailService.Object);
        }


        [Test]
        public void Index_WhenCalled_ShouldReturnIndexWithMessageViewModelInstance()
        {
            //Act & Assert

            _controller.WithCallTo(c => c.Index())
                .ShouldRenderView("index")
                .WithModel<MessageViewModel>();
        }

        [Test]
        public void Emails_WhenCalled_ShouldReturnListOfMessages()
        {//TODO: Update this test for the new conditional
            //Arrange
            //Create a mock Message to be returned in place of DB data.
            List<Message> listOfMessages = new List<Message>();
            listOfMessages.Add(new Message
            {
                Body = "This is a mocked message.",
                Id = 1,
                RecipientEmail = "Test@Test.com",
                SenderEmail = "ToddCullum@gmail.com",
                SenderName = "Todd Cullum",
                Subject = "Test Subject",
                TimeStamp = DateTime.Now
            });
            //Tells the mocked up repo's GetAll action what to return.
            _mockRepository.Setup(r => r.GetAll()).Returns(listOfMessages);


            FormController controller = new FormController(_mockRepository.Object, _mockTheEmailService.Object);

            //Act & Assert
            _controller
                .WithCallTo(c => c.Emails())
                .ShouldRenderDefaultView()
                .WithModel<IList<Message>>();

        }


        [Test]
        public void Save_GivenValidMessage_ShouldReturnReturnIndexViewWithViewModel()
        {
            //
            //Arrange
            Message mockMessage = new Message
            {
                Body = "This is a mocked message.",
                Id = 1,
                RecipientEmail = "Test@Test.com",
                SenderEmail = "ToddCullum@gmail.com",
                SenderName = "Todd Cullum",
                Subject = "Test Subject",
                TimeStamp = DateTime.Now
            };

            //Act & Assert
            _controller.WithCallTo(c => c.Save(mockMessage))
                .ShouldRenderView("index").WithModel<MessageViewModel>();
        }

        [Test]
        public void Save_GivenInvalidMessage_ShouldReturnIndexView()
        {
            //Arrange
            Message mockMessage = null;

            //Act & Assert
            _controller
                .WithCallTo(c => c.Save(mockMessage))
                .ShouldRenderView("index")
                .WithModel<MessageViewModel>();

        }



    }
}
