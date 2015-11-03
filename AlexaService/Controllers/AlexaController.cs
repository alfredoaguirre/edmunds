using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;

//using AlexaSkillsKit.Json;
//using AlexaSkillsKit.Speechlet;

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
        public async Task<string> Post(Json.SpeechletRequestEnvelope requestBody)
        {
            var intentName = requestBody.Request.Intent.Name;

            var Slots = requestBody.Request.Intent.Slots.Children()
                .Select(x =>(JProperty) x)
                .ToDictionary(x => x.Name, x => x.ToString());
            var request = EdmundsClient.Caller.GetRequest(Slots, intentName);
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
