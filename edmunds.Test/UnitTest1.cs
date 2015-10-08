using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace edmunds.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var class1 = new Class1();
            class1.Invoke();
        }
    }
}
