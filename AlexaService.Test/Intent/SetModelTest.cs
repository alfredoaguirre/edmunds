using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaService.Intent;
using System.Collections.Generic;

namespace AlexaService.Test.Intent
{
    [TestClass]
    public class SetModelTest
    {
        [TestInitialize]
        public void Setup()
        {
            IntentBase.UseResponseNumber = 0;
            Cache.CacheManager.Clean();
        }

        //Get Price - all 2 mandatory parameters provided (Make, Model)
        [TestMethod]
        public void setModelTest_1arg()
        {
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Model", "tercel" }
                }
            );
            var intent = new SetModel();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "The selected model is tercel ");
        }
        
        
        //Get Price - all 2 mandatory parameters provided (Make, Model)
        [TestMethod]
        public void setModelTest_0arg()
        {
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                }
            );
            var intent = new SetModel();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "What's the model of the car?");
        }
    
    }
}
