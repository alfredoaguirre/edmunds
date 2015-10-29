using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaService.Controllers;
using System.Net.Http;
using System.Web.Http;

namespace AlexaService.Test
{
    [TestClass]
    public class AlexaServiceTest
    {
        [TestMethod]
        public void GetMPG()
        {
            // Arrange
            var controller = new AlexaController();
            controller.Request = new HttpRequestMessage();
            controller.Request.Content = new HttpContent()
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Post();

            // Assert
        
        }
    }
}
