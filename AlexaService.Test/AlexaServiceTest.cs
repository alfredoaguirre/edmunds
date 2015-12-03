using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaService.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            var responce = controller.Post();
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

        [TestMethod]
        public void GetSlotTest()
        {
            // Arrange
            StreamReader file = new StreamReader(@"payload\event.json");
            var payload = JsonConvert.DeserializeObject<Json.SpeechletRequestEnvelope>(file.ReadToEnd());

            // Act
            var slots = payload.Request.Intent.GetSlots;

            var spected = new Dictionary<string, string>()
            {
                {"model", "Integra"},
                {"make", "Acura"},
                {"year", "2001"}
            };

            // Assert
            foreach (var slotPair  in slots.Zip(spected, (x, y) => new {x, y}))
            {
                Assert.AreEqual(slotPair.x.Key, slotPair.y.Key);
                Assert.AreEqual(slotPair.x.Value, slotPair.y.Value);
            }
        }
    }
}
