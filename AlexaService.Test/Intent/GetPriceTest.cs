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
    public class GetPriceTest
    {
        [TestInitialize]
        public void Setup()
        {
            IntentBase.UseResponseNumber = 0;
            Cache.CacheManager.Clean();
        }

        /* MODEL
        [TestMethod]
         public void getCarPriceTest()
         {
             // Arrange
             AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                 {
                    // {"Make", "Dodge" },
                 //    {"Name", "Dart" },
                   //  {"Year", "2013" }
                 }
             );
             var intent = new GetPrice();
             var edmundsURL = intent.GenEdmundsURL();
             var edmundsResponse = intent.GetEdmundsResponse();
             var AlexaResponse=  intent.getAlexaResponse();

             Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the make of the car?");

         }
         */

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
            var AlexaResponse=  intent.GetAlexaResponse();
            
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
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }
        //Get Price - all 3 parameters provided - mismatched make / model (Year, Make, Model)
        [TestMethod]
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
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }
        //Get Price - all 3 parameters provided - hyphenated make (Year, Make, Model)
        [TestMethod , Ignore]
        public void getCarPriceTest_3hyphen()
        {
            // User says Mercedes Benz. Should match against mercedes-benz
            IntentBase.UseResponseNumber = 1;

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
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "The 2012 Mercedes-Benz S Class has a starting price of 91850.");

        }
        //Get Price - all 3 parameters provided - price not found (Year, Make, Model)
        [TestMethod]
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
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }
        //Get Price - all 3 parameters provided - make not found (Year, Make, Model)
        [TestMethod]
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
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }

        //Get Price - all 3 parameters provided - model not found (Year, Make, Model)
        [TestMethod]
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
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }

        //Get Price - all 3 parameters provided - missing full model info (Year, Make, Model) - //SHOULD WE ASK FOR MODIFIED MODELS?
        [TestMethod]
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
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }


        //Get Price - all 3 parameters provided - missing full year (Year, Make, Model) - 2002
        [TestMethod , Ignore]
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
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "The 2002 Toyota Camry has a starting price of 23700.");

        }


        //Get Price - all 2 mandatory parameters provided (Make, Model) - THIS TEST IS ON HOLD UNTIL TECHNICAL IMPLEMENTATION CAN BE DETERMINED
        /*public void getCarPriceTest_2makemodel()
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

        }*/


        //Get Price - 2 non mandatory parameters provided (Year, Make)
        [TestMethod]
        public void getCarPriceTest_2nonmandatory()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Toyota" },
                    {"Year", "2012" },
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the model of the car?");

        }


        //Get Price - 2 other non mandatory parameters provided (Year, Model)
        [TestMethod]
        public void getCarPriceTest_2nonmandatory2()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Model", "Camry" },
                    {"Year", "2012" },
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the make of the car?");

        }

        //Get Price - only year provided
        [TestMethod]
        public void getCarPriceTest_onlyyear()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Year", "2012" },
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the make of the car?");

        }


        //Get Price - only make provided
        [TestMethod]
        public void getCarPriceTest_onlymake()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Toyota" },
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the model of the car?");

        }


        //Get Price - only model provided
        [TestMethod]
        public void getCarPriceTest_onlymodel()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Model", "Camry" },
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the make of the car?");

        }


        //Get Price - no response - a car already in the memory
        [TestMethod, Ignore]
        public void getCarPriceTest_noresponse()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                   
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "A new 2002 Toyota Camry has a starting price of 23700.");

        }


        //Get Price - nonsense 
        [TestMethod][Ignore]
        public void getCarPriceTest_nonsense()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"jhgfjh","University of the Texas at Arlington" },
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }


        
        
    }
}
