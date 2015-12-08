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
    public class AlexaServiceSelectCar
    {
    
        //Get Car - all 3 parameters provided (Year, Make, Model)
        [TestMethod]
        public void getCarTest_3good()
        {
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "BMW" },
                    {"Model", "5 Series" },
                    {"Year", "2014" }
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "The 5 Series manufactured by BMW was first made in 2014. The last year the 5 Series was made was in 2014.");

        }
        //Get Car - all 3 parameters provided - bad year (Year, Make, Model). ILX started in 2013.
        public void getCarTest_3badyr()
        {
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Acura" },
                    {"Model", "ILX" },
                    {"Year", "2012" }
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "I don't have that car in my records. Hm try again later.");

        }
        //Get Car - all 3 parameters provided - mismatched make / model (Year, Make, Model)
        public void getCarTest_3mismatch()
        {
            // Arrange
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Acura" },
                    {"Model", "Camry" },
                    {"Year", "2012" }
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "I don't have that car in my records. Hm try again later.");

        }
        //Get Car - all 3 parameters provided - hyphenated make (Year, Make, Model)
        public void getCarTest_3hyphen()
        {
            // User says Mercedes Benz. Should match against mercedes-benz
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mercedes Benz" },
                    {"Model", "S Class" },
                    {"Year", "2012" }
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "The 5 Series manufactured by BMW was first made in 2014. The last year the 5 Series was made was in 2012.");

        }
        //Get Car - all 3 parameters provided - price not found (Year, Make, Model)
        /* Couldn't find an example car
        public void getCarTest_3noprice()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mazda" },
                    {"Model", "MX-3" },
                    {"Year", "1995" }
                }
            );
            var intent = new GetCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "Hmm. I can't seem to find the price at this time.");

        }*/
        //Get Car - all 3 parameters provided - make not found (Year, Make, Model)
        public void getCarTest_3badmake()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mazda" },
                    {"Model", "Camry" },
                    {"Year", "1995" }
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "I don't have that car in my records. Hm try again later.");

        }

        //Get Car - all 3 parameters provided - model not found (Year, Make, Model)
        public void getCarTest_3nomodel()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mazda" },
                    {"Model", "Amazon" },
                    {"Year", "1995" }
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "I don't have that car in my records. Hm try again later.");

        }

        //Get Car - all 3 parameters provided - missing full model info (Year, Make, Model) - //SHOULD WE ASK FOR MODIFIED MODELS? - maybe, in a future phase
        public void getCarTest_3notfullmodel()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Mazda" },
                    {"Model", "CX" },
                    {"Year", "2014" }
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "I don't have that car in my records. Hm try again later.");

        }


        //Get Car - all 3 parameters provided - missing full year (Year, Make, Model) - 2002
        public void getCarTest_3notfullyear()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Toyota" },
                    {"Model", "Camry" },
                    {"Year", "02" }
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "The gas mileage of 2002 Toyota Camry is 26 in the city and 18 on the highway");

        }


        //Get Car - all 2 mandatory parameters provided (Make, Model) - THIS TEST IS ON HOLD UNTIL TECHNICAL IMPLEMENTATION CAN BE DETERMINED
        /*public void getCarTest_2makemodel()
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
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "A new 2002 Toyota Camry has a starting price of 23700.");

        }*/


        //Get Car - 2 non mandatory parameters provided (Year, Make)
        public void getCarTest_2nonmandatory()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Toyota" },
                    {"Year", "2012" },
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "What's the model of the car?");
        }


        //Get Car - 2 other non mandatory parameters provided (Year, Model)
        public void getCarTest_2nonmandatory()
        {
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Model", "Camry" },
                    {"Year", "2012" },
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "What's the make of the car?");

        }

        //Get Car - only year provided
        public void getCarTest_onlyyear()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Year", "2012" },
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "What's the make of the car?");

        }


        //Get Car - only make provided
        public void getCarTest_onlymake()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Make", "Toyota" },
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "What's the model of the car?");

        }


        //Get Car - only model provided
        public void getCarTest_onlymodel()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"Model", "Camry" },
                }
            );
            var intent = new SelectCars();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "What's the make of the car?");

        }


        //Get Car - no response - a car already in the memory
        public void getCarTest_noresponse()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                   
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "The gas mileage of 2002 Toyota Camry is 26 in the city and 18 on the highway.");

        }


        //Get Car - nonsense 
        public void getCarTest_nonsense()
        {
            
            AlexaService.Cache.CacheManager.AddSlots(new Dictionary<string, string>()
                {
                    {"m","University of the Texas at Arlington" },
                }
            );
            var intent = new SelectCar();
            var edmundsURL = intent.GenEdmundsURL();
            var edmundsResponse = intent.GetEdmundsResponse();
            var AlexaResponse=  intent.getAlexaResponse();
            
            Assert.AreEqual(AlexaResponse.outputSpeech.text, "I don't know at this time.");

        }


        
        
    }
}
