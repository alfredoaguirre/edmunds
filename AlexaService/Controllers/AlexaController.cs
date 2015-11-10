using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;

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
        public static string Post(Json.SpeechletRequestEnvelope requestBody)
        {
            var intentName = requestBody.Request.Intent.Name;
            var slots = requestBody.Request.Intent.GetSlots;
            var request = EdmundsClient.Caller.GetRequest(slots, intentName);
            return request;
        }

        [Route("alexa/sample-session")]
        [HttpPost]
        public HttpResponseMessage SampleSession()
        {
            return null;
        }

      
    }
}
