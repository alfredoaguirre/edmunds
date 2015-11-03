using EdmundsClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EdmundsClient
{
    public static class Caller
    {
        public static string GetRequest(Dictionary<string, string> inputArgs, string intentName = "")
        {
            WebRequest webRequest = WebRequest.Create(EndPointsManager.GetPath(inputArgs, intentName));
            Stream responseStream = webRequest.GetResponse().GetResponseStream();
            JObject o2;
            using (StreamReader stream = new StreamReader(responseStream))
            {
                using (JsonTextReader reader = new JsonTextReader(stream))
                {
                    o2 = (JObject)JToken.ReadFrom(reader);
                }
            }
            return o2.ToString();
        }
    }
}
