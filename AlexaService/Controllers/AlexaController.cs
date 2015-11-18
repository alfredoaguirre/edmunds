using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
            if (!string.IsNullOrEmpty(intentName))
            {
                var slots = requestBody.Request.Intent.GetSlots;
                var request = EdmundsClient.Caller.GetRequest(slots, intentName);
                request = request.Replace("\n", "").Replace("\r", "");
                return request;
            }
            return null;
        }

        [Route("alexa/sample-session")]
        [HttpPost]
        public HttpResponseMessage SampleSession()
        {
            return null;
        }


    }
}
