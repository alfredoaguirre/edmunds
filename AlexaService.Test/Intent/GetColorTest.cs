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
  
    public class GetColorTest
    {
    
        //Get Color - all 3 parameters provided (Year, Make, Model)
        [TestMethod][Ignore]
        public void getCarColorTest_3good()
        {
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "BMW" },
                    {"Model", "5 Series" },
                    {"Year", "2014" }
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "The gas mileage of 2014 BMW 5 Series is 26 in the city and 37 on the highway");

        }
        //Get Color - all 3 parameters provided - bad year (Year, Make, Model). ILX started in 2013.
        [TestMethod][Ignore]
        public void getCarColorTest_3badyr()
        {
            // Arrange
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Acura" },
                    {"Model", "ILX" },
                    {"Year", "2012" }
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "I don't know at this time.");

        }
        //Get Color - all 3 parameters provided - mismatched make / model (Year, Make, Model)
        [TestMethod][Ignore]
        public void getCarColorTest_3mismatch()
        {
            // Arrange
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Acura" },
                    {"Model", "Camry" },
                    {"Year", "2012" }
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "I don't know at this time.");

        }
        //Get Color - all 3 parameters provided - hyphenated make (Year, Make, Model)
        [TestMethod][Ignore]
        public void getCarColorTest_3hyphen()
        {
            // User says Mercedes Benz. Should match against mercedes-benz
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mercedes Benz" },
                    {"Model", "S Class" },
                    {"Year", "2012" }
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "The gas mileage of 2012 Mercedes Benz S Class is 25 in the city and 19 on the highway");

        }
       
        //Get Color - all 3 parameters provided - make not found (Year, Make, Model)
        [TestMethod][Ignore]
        public void getCarColorTest_3badmake()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mazda" },
                    {"Model", "Camry" },
                    {"Year", "1995" }
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "I don't know at this time.");

        }

        //Get Color - all 3 parameters provided - model not found (Year, Make, Model)
        [TestMethod][Ignore]
        public void getCarColorTest_3nomodel()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mazda" },
                    {"Model", "Amazon" },
                    {"Year", "1995" }
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "I don't know at this time.");

        }

        //Get Color - all 3 parameters provided - missing full model info (Year, Make, Model) - //SHOULD WE ASK FOR MODIFIED MODELS? - maybe, in a future phase
        [TestMethod][Ignore]
        public void getCarColorTest_3notfullmodel()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mazda" },
                    {"Model", "CX" },
                    {"Year", "2014" }
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "I don't know at this time.");

        }


        //Get Color - all 3 parameters provided - missing full year (Year, Make, Model) - 2002
        [TestMethod][Ignore]
        public void getCarColorTest_3notfullyear()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Toyota" },
                    {"Model", "Camry" },
                    {"Year", "02" }
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "The gas mileage of 2002 Toyota Camry is 26 in the city and 18 on the highway");

        }


        //Get MPG - all 2 mandatory parameters provided (Make, Model) - THIS TEST IS ON HOLD UNTIL TECHNICAL IMPLEMENTATION CAN BE DETERMINED
        /*[TestMethod][Ignore]
        public void getCarMPGTest_2makemodel()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Toyota" },
                    {"Model", "Camry" },
                }
            );
            var intent = new GetMileage();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "A new 2002 Toyota Camry has a starting price of 23700.");

        }*/


        //Get Color - 2 non mandatory parameters provided (Year, Make)
        [TestMethod][Ignore]
        public void getCarColorTest_2nonmandatory()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Toyota" },
                    {"Year", "2012" },
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the model of the car?");

        }


        //Get Color - 2 other non mandatory parameters provided (Year, Model)
        [TestMethod][Ignore]
        public void getCarColorTest_2nonmandatory2()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Model", "Camry" },
                    {"Year", "2012" },
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the make of the car?");

        }

        //Get Color - only year provided
        [TestMethod][Ignore]
        public void getCarColorTest_onlyyear()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Year", "2012" },
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the make of the car?");

        }


        //Get Color - only make provided
        [TestMethod][Ignore]
        public void getCarColorTest_onlymake()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Toyota" },
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the model of the car?");

        }


        //Get Color - only model provided
        [TestMethod][Ignore]
        public void getCarColorTest_onlymodel()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Model", "Camry" },
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the make of the car?");

        }


        //Get Color - no response - a car already in the memory
        [TestMethod][Ignore]
        public void getCarColorTest_noresponse()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                   
                }
            );
            var intent = new GetColor();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "The gas mileage of 2002 Toyota Camry is 26 in the city and 18 on the highway.");

        }
    
    }
}
