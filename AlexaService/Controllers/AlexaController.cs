using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AlexaService.Intent;
using AlexaService.Cache;

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
        public string Post()
        {
            Json.SpeechletRequestEnvelope requestBody =
            JsonConvert.DeserializeObject<Json.SpeechletRequestEnvelope>(Request.Content.ReadAsStringAsync().Result);
            var intentName = requestBody?.Request?.Intent?.Name;

            if (string.IsNullOrEmpty(intentName))
            {
                return null;
            }
            CacheManager.AddSlots(requestBody.Request.Intent.GetSlots);
            IntentBase intent = IntentManager.GetIntent(intentName);
            return intent.GetEdmundsResponse();
        }

        [Route("alexa/sample-session")]
        [HttpPost]
        public HttpResponseMessage SampleSession()
        {
            return null;
        }


    }
}
