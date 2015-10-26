using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using EdmundsClient;

namespace EdmundsClient.Test
{
    [TestClass]
    public class EdmundsClientTest
    {
        
        [TestMethod]
        public void testEndPoint()
        {
            var class2 = new EndPoint(Paths.byMakeModelYear);
            Assert.AreEqual(class2.ArgNames[0], "make");
            Assert.AreEqual(class2.ArgNames[1], "model");
            Assert.AreEqual(class2.ArgNames[2], "year");

            class2 = new EndPoint(Paths.byModel);
            Assert.AreEqual(class2.ArgNames[0], "make");
            Assert.AreEqual(class2.ArgNames[1], "model");
        }
        [TestMethod]
        public void testEndManagerPoint()
        {
            var s = EndPointsManager.EndPoints;
        }

        [TestMethod]
        public void testGetEndPoint()
        {
            List<string> argNames = new List<string>()
            {"make", "model" };
            var s = EndPointsManager.GetEndPoint(argNames);
            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void testGetPath()
        {
            Dictionary<string, string> argNames = new Dictionary<string, string>()
            {
                { "make", "toyota" },
                { "model", "corolla" }
            };
            var s = EndPointsManager.GetPath(argNames);
            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void testGetRequest()
        {
            Dictionary<string, string> argNames = new Dictionary<string, string>()
            {
                { "make", "toyota" },
                { "model", "corolla" }
            };
            var s = Caller.GetRequest(argNames);
            Assert.IsNotNull(s);
        }
    }
}
