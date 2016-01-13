using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaService.Intent;
using AlexaService.Intent;
using System.Collections.Generic;

namespace AlexaService.Test.Intent
{
    [TestClass]
    public class CancelTest
    {
        [TestInitialize]
        public void Setup()
        {
            IntentBase.UseResponseNumber = 0;
            Cache.CacheManager.Clean();
        }

        //Cancel Test
        [TestMethod]
        public void CancelTest_1arg()
        {
            var intent = new Cancel();
            var AlexaResponse=  intent.GetAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "Cancelling");
        }    
    }
}
