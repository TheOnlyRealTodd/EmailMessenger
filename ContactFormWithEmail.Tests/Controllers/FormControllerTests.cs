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
        public void Index_Returns_Index_ViewResult()
        {
            //Act
            ViewResult result = _controller.Index();

            //Assert
            Assert.AreEqual("index",result.ViewName);
        }

        [Test]
        public void Index_WhenCalled_ShouldReturnMessageViewModelInstance()
        {
            //Act
            ViewResult result = _controller.Index();

            //Assert
            Assert.IsInstanceOf<MessageViewModel>(result.Model);
        }

        [Test]
        public void Emails_WhenCalled_ShouldReturnListOfMessages()
        {
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

            //Act
            ViewResult result = controller.Emails();

            //Assert
            Assert.IsInstanceOf<List<Message>>(result.Model);

        }

        [Test]
        public void Save_GivenValidMessage_ShouldReturnRedirectToRouteResult()
        {
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

            //Act
            var result = _controller.Save(mockMessage);
            //Assert
            Assert.IsInstanceOf<RedirectToRouteResult>(result);
        }
        [Test]
        public void Save_GivenValidMessage_ShouldReturnRedirectToSendEmail()
        {
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

            //Act - Must cast to RedirectToRouteResult to get RouteValues access
            var result = _controller.Save(mockMessage) as RedirectToRouteResult;
            //Assert
            Assert.AreEqual("SendEmail", result.RouteValues["action"]);
        }

        [Test]
        public void Save_GivenInvalidMessage_ShouldReturnIndexView()
        {
            //Arrange
            Message mockMessage = null;

            //Act - Must cast to RedirectToRouteResult to get RouteValues access
            var result = _controller.Save(mockMessage) as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }



    }
}
