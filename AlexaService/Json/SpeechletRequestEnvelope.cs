using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace AlexaService.Json
{
    public class SpeechletRequestEnvelope
    {
        public Request Request { get; set; }
        public Session Session { get; set; }
        public string Version { get; set; }

    }

    public class Session
    {
        public string sessionId { get; set; }
        public string attributes { get; set; }
        public bool @new { get; set; }
    }

    public class Request
    {
        public string Type { get; set; }
        public string RequestId { get; set; }
        public string Timestamp { get; set; }
        public Intent Intent { get; set; }
    }

    public class Intent
    {
        public string Name { get; set; }
        public JObject Slots { get; set; }

    }

    public class Slot
    {
        
    }
}
