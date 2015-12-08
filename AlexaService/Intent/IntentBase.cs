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


        public string GetPositiveResponseTemplate()
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
            if (string.IsNullOrWhiteSpace(EdmundsUrlTemplate))
            {
                return GetNegativeResponseTemplate();
            }
            var fullResponce = GetEdmundsFullResponse();
            if (!string.IsNullOrWhiteSpace(MissingSlot) || string.IsNullOrWhiteSpace(fullResponce))
            {
                return GetErrorResponse();
            }
            JObject o = JObject.Parse(fullResponce);

            var positiveResponseTemplate = GetPositiveResponseTemplate();

            List<string> arg = new List<string>();
            var maches = Regex.Matches(positiveResponseTemplate, @"(\{(\w*):?(\w*)\})");
            foreach (Match mache in maches)
            {
                if (mache.Groups[2].Value == "slot" && mache.Groups[3].Value != "")
                    arg.Add(CacheManager.Slots[mache.Groups[3].Value]);
                else
                     if (mache.Groups[3].Value == "")
                {
                    var value = o.SelectToken(Response[mache.Groups[2].Value]);
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
        virtual public string GetErrorResponse()
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
        public SpeechletResponse getAlexaResponse()
        {
            return new SpeechletResponse()
            {
                outputSpeech = new OutputSpeech()
                {
                    text = GetEdmundsResponse(),
                },
                reprompt = new Reprompt()
                {
                    outputSpeech = new OutputSpeech()
                    {
                        text = getReprompt()
                    }
                }
            };

        }
    }
}