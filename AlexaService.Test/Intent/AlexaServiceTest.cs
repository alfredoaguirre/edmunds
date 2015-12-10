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
using AlexaService.Intent;

namespace AlexaService.Test
{
    [TestClass]
    public class AlexaServiceTest
    {
        [TestInitialize]
        public void Setup()
        {
            IntentBase.UseResponseNumber = 0;
            Cache.CacheManager.Clean();
        }
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
            foreach (var slotPair in slots.Zip(spected, (x, y) => new { x, y }))
            {
                Assert.AreEqual(slotPair.x.Key, slotPair.y.Key);
                Assert.AreEqual(slotPair.x.Value, slotPair.y.Value);
            }
        }
       
        [TestMethod]
        public void SelectCare2()
        {
            // Arrange
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Dodge" },
                    {"Model", "Dart" },
                    {"Year", "2013" }
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            // Assert.AreEqual("https://api.edmunds.com/api/vehicle/v2/Dodge/Dart/2013/styles?view=full&fmt=json&api_key=67t7jtrnvz8wyzgfpwgcqa3y", edmundsURL);

            var edmundsResponse = intent.GetEdmundsResponse();
            // Assert.AreEqual("Dodge Dart have 36", edmundsResponse);

            // Act

            // Assert

        }
        [TestMethod]
        public void getCarPriceTest()
        {
            // Arrange
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Dodge" },
                    {"Model", "Dart" },
                    {"Year", "2013" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            Assert.AreEqual("https://api.edmunds.com/api/vehicle/v2/Dodge/Dart/2013/styles?view=full&fmt=json&api_key=67t7jtrnvz8wyzgfpwgcqa3y", edmundsURL);

            var edmundsResponse = intent.GetEdmundsResponse();
          var AlexaResponse=  intent.getAlexaResponse();
        }
    }
}
