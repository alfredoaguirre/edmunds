using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        //Set Make - all 1 parameters provided
        [TestMethod]
        public void CancelTest_1arg()
        {
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Dodge" }
                }
            );
            var intent = new SetMake();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "The selected make is Dodge");
        }    
    }
}
