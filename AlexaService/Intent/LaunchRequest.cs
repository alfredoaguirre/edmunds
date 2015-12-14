using AlexaService.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Intent
{
    class LaunchRequest : IntentBase
    {
        public LaunchRequest()
        {
            Name = "LaunchRequest";
        }
        public SpeechletResponseEnvelope getAlexaResponse()
        {
            return new SpeechletResponseEnvelope()
            {
                response = new SpeechletResponse()
                {
                    outputSpeech = new OutputSpeech()
                    {
                        text = "hi how you doing"
                    },
                    reprompt = new Reprompt()
                    {
                        outputSpeech = new OutputSpeech()
                        {
                            text ="hello"
                        }
                    },
                    shouldEndSession = false,
                    card = new Card()
                },
                version = "1.0",
            };
        }
    }
}
