using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaService.Intent;
using System.Collections.Generic;

namespace AlexaService.Test.Intent
{
    [TestClass]
    public class SelectYearTest
    {
        [TestInitialize]
        public void Setup()
        {
            IntentBase.UseResponseNumber = 0;
            Cache.CacheManager.Clean();
        }

        //Set Year - all 1 mandatory parameters provided
        [TestMethod]
        public void setYearTest_1arg()
        {
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Year", "2015" }
                }
            );
            var intent = new SetYear();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "The selected year is 2015");
        }
        
        //Set Year - Missing parameter
        [TestMethod]
        public void setYearTest_0arg()
        {
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                }
            );
            var intent = new SetYear();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "There is no selected year");
        }

    }
}
