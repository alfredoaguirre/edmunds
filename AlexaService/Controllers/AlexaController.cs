using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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
        public async Task<String> Post()
        {
            var payload =  await Request.Content.ReadAsByteArrayAsync();
            string result = System.Text.Encoding.UTF8.GetString(payload);
            Console.WriteLine(result);
            return "hola";
        }
        [Route("alexa/sample-session")]
        [HttpPost]
        public HttpResponseMessage SampleSession()
        {
            var speechlet = new SampleSessionSpeechlet();
            var re = speechlet.GetResponse(Request);
            return null;
        }
    }
}
 