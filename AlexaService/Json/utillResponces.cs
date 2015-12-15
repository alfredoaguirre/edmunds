using AlexaService.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaService.Json
{
    public static class UtillResponces
    {
        public static SpeechletResponseEnvelope NoIntent(string name)
        {
            return new SpeechletResponseEnvelope()
            {
                response = new SpeechletResponse()
                {
                    outputSpeech = new OutputSpeech()
                    {
                        text = "Error there is no intent for  " + name
                    },
                    reprompt = new Reprompt()
                    {
                        outputSpeech = new OutputSpeech()
                        {
                            text = "Car details provides information about cars! What car would you like to know more about?"
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
