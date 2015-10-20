using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
