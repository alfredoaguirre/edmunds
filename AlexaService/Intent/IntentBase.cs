using System;
using System.Collections.Generic;
using System.Linq;
using AlexaService.Cache;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using AlexaService.Json;

namespace AlexaService.Intent
{
    public class IntentBase
    {
        public string Name { get; set; }
        public string BasePath { get; set; }
        public string MissingSlot { get; set; }
        public static int? UseResponseNumber { get; set; }
        public string EdmundsUrlTemplate { get; set; }
        public Dictionary<string, string> FollowingQuestiestionMissingSlot { get; set; }
        static Random random = new Random();
        public List<string> PositiveResponseTemplate { get; private set; }
        public List<string> NegativeResponseTemplate { get; private set; }
        public Dictionary<string, string> ErrorSlotResponse { get; private set; }
        public Dictionary<string, string> Response { get; private set; }

        public IntentBase()
        {
            BasePath = "";
            PositiveResponseTemplate = new List<string>();
            Response = new Dictionary<string, string>();
            ErrorSlotResponse = new Dictionary<string, string>();
            NegativeResponseTemplate = new List<string>();
            FollowingQuestiestionMissingSlot = new Dictionary<string, string>();
        }
        virtual public string GenEdmundsURL()
        {
            if (string.IsNullOrWhiteSpace(EdmundsUrlTemplate))
            {
                return null;
            }
            List<string> arg = new List<string>();
            var maches = Regex.Matches(EdmundsUrlTemplate, @"(\{(\w*):(\w*)\})");
            foreach (Match mache in maches)
            {
                if (mache.Groups[2].Value == "slot")
                {
                    if (CacheManager.Slots.Keys.Any(x => x == (mache.Groups[3].Value)))
                        arg.Add(CacheManager.Slots[mache.Groups[3].Value]);
                    else
                    {
                        MissingSlot = mache.Groups[3].Value;
                        return null;
                    }
                }
                else
                    throw new Exception();
            }
            var r = new Regex(@"(\{(\w*:\w*)\})");
            int count = 0;
            var url = r.Replace(EdmundsUrlTemplate, x => "{" + count++ + "}");
            url = String.Format(url, arg.ToArray());
            return url;
        }

        virtual public string GetEdmundsFullResponse()
        {
            var url = GenEdmundsURL();
            if (string.IsNullOrWhiteSpace(url))
                return "";
            return EdmundsClient.Caller.GetRequest(url);
        }

        virtual public string GetPositiveResponseTemplate()
        {
            if (UseResponseNumber != null)
                return PositiveResponseTemplate[UseResponseNumber.Value];
            return PositiveResponseTemplate[random.Next(0, PositiveResponseTemplate.Count)];
        }

        public string GetNegativeResponseTemplate()
        {
            if (UseResponseNumber != null)
                return NegativeResponseTemplate[UseResponseNumber.Value];
            return NegativeResponseTemplate[random.Next(0, PositiveResponseTemplate.Count)];
        }

        virtual public string GetEdmundsResponse()
        {
            JObject EdmundsJson = null;
            string EdmundsResponse = "";
            if (string.IsNullOrWhiteSpace(EdmundsUrlTemplate))
            {
            }
            else
            {
                 EdmundsResponse = GetEdmundsFullResponse();
              
            }
            if (!string.IsNullOrWhiteSpace(MissingSlot))
            {
                return GetErrorMissingSlotResponse();

            }  if ( string.IsNullOrWhiteSpace( EdmundsResponse) && !string.IsNullOrWhiteSpace(EdmundsUrlTemplate))
            {
                return GetNegativeResponseTemplate();
            }
            if (!string.IsNullOrWhiteSpace(EdmundsResponse))
            {
                EdmundsJson = JObject.Parse(EdmundsResponse);
            }
            var positiveResponseTemplate = GetPositiveResponseTemplate();

            List<string> arg = new List<string>();
            var maches = Regex.Matches(positiveResponseTemplate, @"(\{(\w*):?(\w*)\})");
            foreach (Match mache in maches)
            {
                if (mache.Groups[2].Value == "slot" && mache.Groups[3].Value != "")

                {
                    var slotKey = mache.Groups[3].Value;
                    if (CacheManager.Slots.Keys.Any(x => x == slotKey))
                    {
                        arg.Add(CacheManager.Slots[mache.Groups[3].Value]);
                    }
                    else
                    {
                        this.MissingSlot = slotKey;
                        return GetErrorMissingSlotResponse();
                    }
                }
                else if (mache.Groups[3].Value == "")
                {
                    if (EdmundsJson == null)
                        return GetNegativeResponseTemplate();
                    var value = EdmundsJson.SelectToken(Response[mache.Groups[2].Value]);
                    if (value == null)
                        return GetNegativeResponseTemplate();
                    arg.Add(value.ToString());
                }
                else
                    throw new Exception();
            }
            var r = new Regex(@"(\{(\w*:?\w*)\})");
            int count = 0;
            var positiveResponse = r.Replace(positiveResponseTemplate, x => "{" + count++ + "}");
            positiveResponse = String.Format(positiveResponse, arg.ToArray());
            return positiveResponse;
        }

        virtual public string GetErrorMissingSlotResponse()
        {
            return ErrorSlotResponse[MissingSlot];
        }


        public string getReprompt()
        {
            if (!string.IsNullOrEmpty(MissingSlot))
                return FollowingQuestiestionMissingSlot[MissingSlot];
            else
                return "";
        }

        public SpeechletResponseEnvelope getAlexaResponse()
        {
            // reste misising slot
            MissingSlot = null;
            return new SpeechletResponseEnvelope()
            {
                response = new SpeechletResponse()
                {
                    outputSpeech = new OutputSpeech()
                    {
                        text = GetEdmundsResponse(),
                    },
                    reprompt = new Reprompt()
                    {
                        outputSpeech = new OutputSpeech()
                        {
                            text = ""// getReprompt()
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