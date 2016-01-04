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
        public static int? UseResponseNumber { get; set; }

        protected string MissingSlot { get; set; }

        protected string EdmundsUrlTemplate { get; set; }

        protected List<string> PositiveResponseTemplate { get; private set; }
        protected List<string> PositiveCardsResponseTemplate { get; private set; }
        protected List<string> NegativeResponseTemplate { get; private set; }
        protected Dictionary<string, string> ErrorSlotResponse { get; private set; }
        protected Dictionary<string, string> Response { get; private set; }
        protected Dictionary<string, string> FollowingQuestiestionMissingSlot { get; set; }

        private static string key = "67t7jtrnvz8wyzgfpwgcqa3y";
        private static string basePath = "https://api.edmunds.com/";

        private static Random random = new Random();

        public IntentBase()
        {
            PositiveResponseTemplate = new List<string>();
            Response = new Dictionary<string, string>();
            ErrorSlotResponse = new Dictionary<string, string>();
            NegativeResponseTemplate = new List<string>();
            FollowingQuestiestionMissingSlot = new Dictionary<string, string>();
        }

        virtual public string GenEdmundsURL()
        {
            if (string.IsNullOrWhiteSpace(EdmundsUrlTemplate))
                return null;
            return basePath + FillTemplate(EdmundsUrlTemplate, null) + "&api_key=" + key;
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
            return GetResponse(PositiveResponseTemplate);

        }
        public string GetNegativeResponseTemplate()
        {
            return GetResponse(NegativeResponseTemplate);
        }

        private string GetResponse(List<string> responses)
        {
            if (UseResponseNumber != null)
                return responses[UseResponseNumber.Value];
            return responses[random.Next(0, responses.Count - 1)];
        }

        virtual public string GetEdmundsResponse()
        {
            JObject EdmundsJson = null;
            string EdmundsResponse = "";
            if (!string.IsNullOrWhiteSpace(EdmundsUrlTemplate))
                EdmundsResponse = GetEdmundsFullResponse();
            if (!string.IsNullOrWhiteSpace(MissingSlot))
                return GetErrorMissingSlotResponse();

            if (string.IsNullOrWhiteSpace(EdmundsResponse) && !string.IsNullOrWhiteSpace(EdmundsUrlTemplate))
                return GetNegativeResponseTemplate();

            if (!string.IsNullOrWhiteSpace(EdmundsResponse))
                EdmundsJson = JObject.Parse(EdmundsResponse);

            var positiveResponseTemplate = GetPositiveResponseTemplate();
            return FillTemplate(positiveResponseTemplate, EdmundsJson);
        }


        protected string FillTemplate(string Temaplate, JObject json = null)
        {
            var arg = new List<string>();
            var maches = Regex.Matches(Temaplate, @"(\{(\w*):?(\w*)\})");
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
                        MissingSlot = slotKey;
                        return GetErrorMissingSlotResponse();
                    }
                }
                else if (mache.Groups[3].Value == "")
                {
                    if (json == null)
                        return GetNegativeResponseTemplate();
                    var value = json.SelectToken(Response[mache.Groups[2].Value]);
                    if (value == null)
                        return GetNegativeResponseTemplate();
                    arg.Add(value.ToString());
                }
                else
                    throw new Exception();
            }
            var r = new Regex(@"(\{(\w*:?\w*)\})");
            int count = 0;
            var positiveResponse = r.Replace(Temaplate, x => "{" + count++ + "}");
            return string.Format(positiveResponse, arg.ToArray());
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
        protected virtual Card getCard()
        {
            return new Card { content = "" };
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
                    card = getCard()
                },
                version = "1.0",
            };
        }
    }
}