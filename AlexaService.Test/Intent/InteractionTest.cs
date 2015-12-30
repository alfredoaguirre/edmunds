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

namespace AlexaService.Test.Intent
{
    [TestClass]
    public class GetInteractionTest
    {
        [TestInitialize]
        public void Setup()
        {
            IntentBase.UseResponseNumber = 0;
            Cache.CacheManager.Clean();
        }

        
        //Get Price - 2 other non mandatory parameters provided (Year, Model)
        [TestMethod]
        public void getCarPriceTest_2nonmandatory2()
        {
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Model", "Camry" },
                    {"Year", "2002" },
                }
            );
            var intent = new GetPrice();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the make of the car?");
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Toyota" }
                }
            );
            
            AlexaResponse = intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "A new 2002 Toyota Camry has a starting price of 25405");
        }
        
    }
}
