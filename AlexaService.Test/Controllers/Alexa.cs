using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AlexaService.Json;
using System.Net.Http.Formatting;
using AlexaService.Intent;

namespace OwinApplicationTesting
{
    [TestClass]
    public class AlexaTest
    {
        protected TestServer server;


        [TestInitialize]
        public void Setup()
        {  IntentBase.UseResponseNumber = 1;
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
            IntentBase.UseResponseNumber = 0;
            StreamReader file = new StreamReader(@"payload\GetPrice for Price for 2013 Toyota Camry.json");
            var clas = JsonConvert.DeserializeObject<SpeechletRequestEnvelope>(file.ReadToEnd());
            var result = await GetPostRequest(clas);

            Assert.AreEqual(result.outputSpeech.text, "A new 2015 Toyota Camry starts at 23840 dollars");
        }
    }
}