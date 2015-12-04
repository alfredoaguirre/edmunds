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
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "What's the make of the car?");

        }
        */
        
        //Get Price - all 3 parameters provided (Year, Make, Model)
        [TestMethod]
        public void getCarPriceTest_3good()
        {
            // Arrange
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "BMW" },
                    {"Name", "5 Series" },
                    {"Year", "2014" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "The 2015 BMW 5 Series has a starting price of 58900");

        }
        //Get Price - all 3 parameters provided - bad year (Year, Make, Model). ILX started in 2013.
        public void getCarPriceTest_3badyr()
        {
            // Arrange
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Acura" },
                    {"Name", "ILX" },
                    {"Year", "2012" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }
        //Get Price - all 3 parameters provided - mismatched make / model (Year, Make, Model)
        public void getCarPriceTest_3mismatch()
        {
            // Arrange
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Acura" },
                    {"Name", "Camry" },
                    {"Year", "2012" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }
        //Get Price - all 3 parameters provided - hyphenated make (Year, Make, Model)
        public void getCarPriceTest_3hyphen()
        {
            // User says Mercedes Benz. Should match against mercedes-benz
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mercedes Benz" },
                    {"Name", "S Class" },
                    {"Year", "2012" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "The 2012 Mercedes-Benz S Class has a starting price of 158050.");

        }
        //Get Price - all 3 parameters provided - price not found (Year, Make, Model)
        public void getCarPriceTest_3noprice()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mazda" },
                    {"Name", "MX-3" },
                    {"Year", "1995" }
                }
            );
            var intent = new GetPrice();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }
        //Get Price - all 3 parameters provided - make not found (Year, Make, Model)
        //Get Price - all 3 parameters provided - model not found (Year, Make, Model)
        //Get Price - all 3 parameters provided - missing full model info (Year, Make, Model)
        //Get Price - all 3 parameters provided - missing full year (Year, Make, Model)
        //Get Price - all 2 mandatory parameters provided (Make, Model)
        //Get Price - 2 non mandatory parameters provided (Year, Make)
        //Get Price - 2 other non mandatory parameters provided (Year, Model)
        //Get Price - only year provided
        //Get Price - only make provided
        //Get Price - only model provided
        //Get Price - no response
        //Get Price - nonsense 
        
        
        
    }
}
