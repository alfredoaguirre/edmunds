using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaService.Intent;
using System.Collections.Generic;

namespace AlexaService.Test.Intent
{
    [TestClass]
    public class StopTest
    {
        [TestInitialize]
        public void Setup()
        {
            IntentBase.UseResponseNumber = 0;
            Cache.CacheManager.Clean();
        }

        //Stop Test
        [TestMethod]
        public void StopTest_1arg()
        {
            var intent = new Stop();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Car Details Stopping");
        }    
    }
}
