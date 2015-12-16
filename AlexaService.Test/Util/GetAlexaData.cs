using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;

namespace AlexaService.Test.Util
{
    [TestClass]
    public class GetAlexaData
    {
        [TestMethod]
        public void getAllMake()
        {
            StreamReader file = new StreamReader(@"Util\AllMaker.json");
          var allmeker =   JsonConvert.DeserializeObject<JObject>(file.ReadToEnd());
            var makerList = from m in allmeker["makes"]
                            select m["name"];
            StringBuilder s = new StringBuilder();
            foreach (var n in makerList)
                s.AppendLine(n.ToString());
        }
        [TestMethod]
        public void getAllModel()
        {
            StreamReader file = new StreamReader(@"Util\AllMaker.json");
            var allmeker = JsonConvert.DeserializeObject<JObject>(file.ReadToEnd());
            var makerList =( from make in allmeker["makes"]
                            from model in make["models"]
                            select model["name"].ToString()).Distinct();

            StringBuilder s = new StringBuilder();
            foreach (var n in makerList)
                s.AppendLine(n.ToString());
        }
    }
}
