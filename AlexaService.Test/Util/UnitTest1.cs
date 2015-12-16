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
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            StreamReader file = new StreamReader(@"Util\AllMaker.json");
          var allmeker =   JsonConvert.DeserializeObject<JObject>(file.ReadToEnd());
            var makerList = from m in allmeker["makes"]
                            select m["name"];
            StringBuilder s = new StringBuilder();
            foreach (var n in makerList)
                s.AppendLine(n.ToString());
        }
    }
}
