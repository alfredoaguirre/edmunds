using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexaService.Json
{
    public class SpeechletResponse
    {
        public OutputSpeech outputSpeech { get; set; }
        public Reprompt reprompt { get; set; }
        public bool shouldEndSession { get; set; }
    }
    public class OutputSpeech
    {
        public string type { get; set; }
        public string text { get; set; }
        public OutputSpeech()
        {
            type = "PlainText";
        }
            }
    public class Reprompt
    {
        public OutputSpeech outputSpeech { get; set; }
    }
}
