using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlexaService.Json
{
    public class SpeechletResponseEnvelope
    {
        public string version { get; set; }
        public SpeechletResponse response { get; set; }
       // public JObject sessionAttributes { get; set; }
    }

    public class buildResponse
    {
        public string version { get; set; }
        public string SpeechletResponse { get; set; }
        public SpeechletResponse response { get; set; }
     
    }
    public class Card
    {
        public string type = "Simple";
        public string title = "HelloWorld";
        public string content = "Welcome to the Alexa Skills Kit, you can say hello";
    }
    public class SpeechletResponse
    {
        public Card card { get; set; }
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
