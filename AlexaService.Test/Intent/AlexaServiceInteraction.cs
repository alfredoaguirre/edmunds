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
    public class AlexaServiceInteraction
    {
        [TestInitialize]
        public void Setup()
        {
            IntentBase.UseResponseNumber = 0;
            Cache.CacheManager.Clean();
        }

        //Get Price - all 3 parameters provided (Year, Make, Model) - //SHOULD WE STATE "A new" instead of The 2002..etc. Cuz old cars look expensive
        [TestMethod]
        public void getCarPriceTest_3good()
        {
            // Arrange
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "BMW" },
                    {"Model", "5 Series" },
                    {"Year", "2014" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "A new 2014 BMW 5 Series has a starting price of 63900");

        }
        //Get Price - all 3 parameters provided - bad year (Year, Make, Model). ILX started in 2013.
        public void getCarPriceTest_3badyr()
        {
            // Arrange
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Acura" },
                    {"Model", "ILX" },
                    {"Year", "2012" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }
        //Get Price - all 3 parameters provided - mismatched make / model (Year, Make, Model)
        public void getCarPriceTest_3mismatch()
        {
            // Arrange
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Acura" },
                    {"Model", "Camry" },
                    {"Year", "2012" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }
        //Get Price - all 3 parameters provided - hyphenated make (Year, Make, Model)
        public void getCarPriceTest_3hyphen()
        {
            // User says Mercedes Benz. Should match against mercedes-benz
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mercedes Benz" },
                    {"Model", "S Class" },
                    {"Year", "2012" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "The 2012 Mercedes-Benz S Class has a starting price of 158050.");

        }
        //Get Price - all 3 parameters provided - price not found (Year, Make, Model)
        public void getCarPriceTest_3noprice()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mazda" },
                    {"Model", "MX-3" },
                    {"Year", "1995" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }
        //Get Price - all 3 parameters provided - make not found (Year, Make, Model)
        public void getCarPriceTest_3badmake()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mazda" },
                    {"Model", "Camry" },
                    {"Year", "1995" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }

        //Get Price - all 3 parameters provided - model not found (Year, Make, Model)
        public void getCarPriceTest_3nomodel()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mazda" },
                    {"Model", "Amazon" },
                    {"Year", "1995" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }

        //Get Price - all 3 parameters provided - missing full model info (Year, Make, Model) - //SHOULD WE ASK FOR MODIFIED MODELS?
        public void getCarPriceTest_3notfullmodel()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mazda" },
                    {"Model", "CX" },
                    {"Year", "2014" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }


        //Get Price - all 3 parameters provided - missing full year (Year, Make, Model) - 2002
        public void getCarPriceTest_3notfullyear()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Toyota" },
                    {"Model", "Camry" },
                    {"Year", "02" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "The 2002 Toyota Camry has a starting price of 23700.");

        }


        //Get Price - all 2 mandatory parameters provided (Make, Model)
        public void getCarPriceTest_2makemodel()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Toyota" },
                    {"Model", "Camry" },
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "A new 2002 Toyota Camry has a starting price of 23700.");

        }


        //Get Price - 2 non mandatory parameters provided (Year, Make)
        //Get Price - 2 other non mandatory parameters provided (Year, Model)
        //Get Price - only year provided
        //Get Price - only make provided
        //Get Price - only model provided
        //Get Price - no response
        //Get Price - nonsense 
        
        
        
    }
}
