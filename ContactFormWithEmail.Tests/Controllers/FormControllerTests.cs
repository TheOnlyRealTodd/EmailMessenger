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

        [Test]
        public void Index_Returns_Index_ViewResult()
        {
            //Arrange
            Mock<MessageRepository> mockRepository = new Mock<MessageRepository>();
            Mock<TheEmailService> mockTheEmailService = new Mock<TheEmailService>();
            FormController controller = new FormController(mockRepository.Object,mockTheEmailService.Object);

            //Act
            ViewResult result = controller.Index();

            //Assert
            Assert.AreEqual("index",result.ViewName);
        }

        [Test]
        public void Index_Returns_MessageViewModel_Instance()
        {
            //Arrange
            Mock<MessageRepository> mockRepository = new Mock<MessageRepository>();
            Mock<TheEmailService> mockTheEmailService = new Mock<TheEmailService>();
            FormController controller = new FormController(mockRepository.Object, mockTheEmailService.Object);

            //Act
            ViewResult result = controller.Index();

            //Assert
            Assert.IsInstanceOf<MessageViewModel>(result.Model);
        }

        [Test]
        public void Emails_Returns_ListOfMessages()
        {
            //Arrange
            Mock<IRepository<Message>> mockRepository = new Mock<IRepository<Message>>();
            Mock<ITheEmailService> mockTheEmailService = new Mock<ITheEmailService>();
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
            mockRepository.Setup(r => r.GetAll()).Returns(listOfMessages);


            FormController controller = new FormController(mockRepository.Object, mockTheEmailService.Object);

            //Act
            ViewResult result = controller.Emails();

            //Assert
            Assert.IsInstanceOf<List<Message>>(result.Model);

        }

    }
}
