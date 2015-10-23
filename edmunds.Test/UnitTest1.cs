using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Edmunds;

namespace Edmunds.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var class1 = new Class1();
            //class1.Invoke();
        }
        [TestMethod]
        public void testEndPoint()
        {
            // var class1 = new Edmunds.EndPoint(Edmunds.Paths.byMaker);
            var class2 = new EndPoint(Paths.byMakeModelYear);
            Assert.AreEqual(class2.argNames[0], "{make}");
            Assert.AreEqual(class2.argNames[1], "{model}");
            Assert.AreEqual(class2.argNames[2], "{year}");

            class2 = new EndPoint(Paths.byModel);
            Assert.AreEqual(class2.argNames[0], "{make}");
            Assert.AreEqual(class2.argNames[1], "{model}");
        }
        [TestMethod]
        public void testEndManagerPoint()
        {
            var s = EndPointsManager.EndPoints;
        }
    }
}
