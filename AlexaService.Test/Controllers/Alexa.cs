using Microsoft.Owin.Hosting;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using System;
using System.Net.Http;
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
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using AlexaService.Json;
using System.Net.Http.Formatting;

namespace OwinApplicationTesting
{
    [TestClass]
    public class SelfHostingTest
    {
        protected TestServer server;

        [TestInitialize]
        public void Setup()
        {
            server = TestServer.Create(app =>
            {
                HttpConfiguration config = new HttpConfiguration();
                AlexaService.WebApiConfig.Register(config);
                app.UseWebApi(config);
            });
        }

        [TestCleanup]
        public void TearDown()
        {
            if (server != null)
                server.Dispose();
        }

        [TestMethod]
        public async Task AlexaGetTest()
        {
            HttpResponseMessage response = await server.CreateRequest("/Alexa").GetAsync();


            var result = await response.Content.ReadAsAsync<string>();

            Assert.AreEqual(result, "alfredo");
        }

        public async Task<SpeechletResponse> GetPostRequest(SpeechletRequestEnvelope requestMsg)
        {
            HttpResponseMessage response = await server.CreateRequest("/Alexa")
                .And(request => request.Content = new ObjectContent(typeof(SpeechletRequestEnvelope), requestMsg, new JsonMediaTypeFormatter()))
                .PostAsync();

            return await response.Content.ReadAsAsync<SpeechletResponse>();
        }


        [TestMethod]
        public async Task AlexaPostTest()
        {
            StreamReader file = new StreamReader(@"payload\GetPrice for Price for 2013 Toyota Camry.json");
            var clas = JsonConvert.DeserializeObject<SpeechletRequestEnvelope>(file.ReadToEnd());
            var result = await GetPostRequest(clas);

            Assert.AreEqual(result.outputSpeech.text, "A new 2015 Toyota Camry starts at 23840 dollars");
        }
    }
}