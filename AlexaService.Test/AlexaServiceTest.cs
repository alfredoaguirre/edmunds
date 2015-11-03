using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaService.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;

namespace AlexaService.Test
{
    [TestClass]
    public class AlexaServiceTest
    {
        [TestMethod]
        public void GetMpg()
        {
            // Arrange
            var controller = new AlexaController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            StreamReader file = new StreamReader(@"payload\event.json");
          
            var clas = JsonConvert.DeserializeObject<Json.SpeechletRequestEnvelope>(file.ReadToEnd());
            var responce = controller.Post(clas);
            // Assert

        }
        [TestMethod]
        public void Getjsone()
        {
            // Arrange
            var controller = new AlexaController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            StreamReader file = new StreamReader(@"payload\event.json");
            var clas = JsonConvert.DeserializeObject<Json.SpeechletRequestEnvelope>(file.ReadToEnd());

            // Assert
            Assert.IsNotNull(clas);
        }
    }
}
