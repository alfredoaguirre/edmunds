using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EdmundsClient
{
    public class Class1
    {
        public void Invoke()

        {
            WebRequest webRequest =
                WebRequest.Create(
                    "https://api.edmunds.com/api/vehicle/v2/honda/accord?state=new&year=2014&view=basic&fmt=json&api_key=67t7jtrnvz8wyzgfpwgcqa3y");
            Stream requestStream = webRequest.GetResponse().GetResponseStream();
            JObject o2;
            using (StreamReader file = new StreamReader(requestStream))

            using (JsonTextReader reader = new JsonTextReader(file))
            {
                o2 = (JObject) JToken.ReadFrom(reader);
            }
            var firstId = o2["years"].First()["id"];
        }
    }
}
