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
        [TestMethod]
        public void TestMethod1()
        {
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "BMW" },
                    {"Model", "5 Series" },
                    {"Year", "2014" }
                }
            );
            var intent = new SelectCar();
            var AlexaResponse = intent.getAlexaResponse();

            Assert.AreEqual(AlexaResponse.response.outputSpeech.text, "The 5 Series manufactured by BMW was first made in 2014. The last year the 5 Series was made was in 2014.");

        }

    }
}
