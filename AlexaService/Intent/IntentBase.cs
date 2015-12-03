using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlexaService.Cache;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace AlexaService.Intent
{
    public class IntentBase
    {
        public string Name { get; set; }
        public string BasePath { get; set; }
        public string MissingSlot { get; set; }

        public string EdmundsUrlTemplate { get; set; }
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
        virtual public string GetTextResponse()
        {
            return null;
        }

        virtual public string GetEdmundsFullResponse()
        {
            var url = GenEdmundsURL();

            return EdmundsClient.Caller.GetRequest(url);
        }

        virtual public string GetEdmundsResponse()
        {
            JObject o;
            if (string.IsNullOrWhiteSpace(EdmundsUrlTemplate))
            {
                o = new JObject();
            }
            else
            {
                var fullResponce = GetEdmundsFullResponse();
                if (string.IsNullOrWhiteSpace(MissingSlot))
                {
                    return GetErrorResponse();
                }
                o = JObject.Parse(fullResponce);
            }
            var positiveResponseTemplate = PositiveResponseTemplate[random.Next(0, PositiveResponseTemplate.Count)];

            List<string> arg = new List<string>();
            var maches = Regex.Matches(positiveResponseTemplate, @"(\{(\w*):?(\w*)\})");
            foreach (Match mache in maches)
            {
                if (mache.Groups[2].Value == "slot" && mache.Groups[3].Value != "")
                    arg.Add(CacheManager.Slots[mache.Groups[3].Value]);
                else
                     if (mache.Groups[3].Value == "")
                    arg.Add(o.SelectToken(Response[mache.Groups[2].Value]).ToString());
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
    }