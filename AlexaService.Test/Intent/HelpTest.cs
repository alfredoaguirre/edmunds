using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaService.Intent;
using System.Collections.Generic;

namespace AlexaService.Test.Intent
{
    [TestClass]
    public class HelpTest
    {
        [TestInitialize]
        public void Setup()
        {
            IntentBase.UseResponseNumber = 0;
            Cache.CacheManager.Clean();
        }

        //Help Test
        [TestMethod]
        public void HelpTest_1arg()
        {
            var intent = new Help();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What car would you like to know more about?");
        }    
    }
}
