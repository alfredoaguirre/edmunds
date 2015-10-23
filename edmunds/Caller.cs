using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EdmundsService
{
   public static  class Caller
    {
        public static String GetRequest(Dictionary<string, string> InputArgs)
        {
            WebRequest webRequest = WebRequest.Create(GetEndPointPath(InputArgs));
                  
            Stream requestStream = webRequest.GetResponse().GetResponseStream();
            JObject o2;
            using (StreamReader file = new StreamReader(requestStream))

            using (JsonTextReader reader = new JsonTextReader(file))
            {
                o2 = (JObject)JToken.ReadFrom(reader);
            }
            var firstId = o2["years"].First()["id"];
            return "";
        }
        public static string GetEndPointPath(Dictionary<string, string> InputArgs)
        {
            StringBuilder endPoint = new StringBuilder();
            endPoint.Append(Paths.url);


            endPoint.Append(Args.jsonFormat);
            endPoint.Append(Args.ApiKey);
            return endPoint.ToString();
        }
    }
}
