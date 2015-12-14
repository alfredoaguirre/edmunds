using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AlexaService.Intent;
using AlexaService.Cache;
using AlexaService.Json;

namespace AlexaService.Controllers
{
    public class AlexaController : ApiController
    {
        [Route("Alexa")]
        [HttpGet]
        public string get()
        {
            return "alfredo";
        }

        [Route("Alexa")]
        [HttpPost]
        public SpeechletResponseEnvelope Post()
        {
            var requestBody = JsonConvert.DeserializeObject<SpeechletRequestEnvelope>(Request.Content.ReadAsStringAsync().Result);
            var type = requestBody?.Request?.Type;
            if (type == "LaunchRequest")
            {
                return LaunchRequest.getAlexaResponse();
            }
            var intentName = requestBody?.Request?.Intent?.Name;

            if (string.IsNullOrEmpty(intentName))
            {
                return null;
            }
            CacheManager.AddSlots(requestBody.Request.Intent.GetSlots);
            IntentBase intent = IntentManager.GetIntent(intentName);
            var alexaResponse = intent.getAlexaResponse();
            CacheManager.Intent.Push(intent);
            return alexaResponse;
        }

        [Route("alexa/sample-session")]
        [HttpPost]
        public HttpResponseMessage SampleSession()
        {
            return null;
        }
    }
}
