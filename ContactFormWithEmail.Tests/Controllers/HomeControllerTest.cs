using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactFormWithEmail;
using ContactFormWithEmail.Controllers;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ContactFormWithEmail.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index_Returns_RedirectResult()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            RedirectToRouteResult result = controller.Index() as RedirectToRouteResult;
            
            // Assert - Testing the route value since the return type is a RedirectToAction
            Assert.AreEqual("Index",result.RouteValues["action"]);
            Assert.AreEqual("Form", result.RouteValues["controller"]);
        }

        [Test]
        public void About_Returns_HttpNotFoundResult()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ActionResult result = controller.About() as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Contact_Returns_HttpNotFoundResult()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ActionResult result = controller.Contact() as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
