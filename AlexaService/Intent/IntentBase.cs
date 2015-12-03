using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlexaService.Cache;

namespace AlexaService.Intent
{
    public class IntentBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
        static Random random = new Random();
        public List<string> ResponseTemplate { get; protected set; }
        public Dictionary<string, string> ResponceSlots { get; protected set; }

        public IntentBase()
        {

        }
        virtual public string GetTextResponse()
        {
            return ResponseTemplate[random.Next(1, ResponseTemplate.Count)];
        }

        virtual public string GetResponse()
        {
            return EdmundsClient.Caller.GetRequest(CacheManager.Slots, Name);
        }
    }
}