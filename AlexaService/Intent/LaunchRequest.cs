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
                        text = "What car would you like to know more about?"
                    },
                    reprompt = new Reprompt()
                    {
                        outputSpeech = new OutputSpeech()
                        {
                            text ="Car details provides information about cars! What car would you like to know more about?"
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
