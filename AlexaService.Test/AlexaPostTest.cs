using Microsoft.Owin.Hosting;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using System;
using System.Net.Http;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlexaService.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using AlexaService.Intent;

namespace OwinApplicationTesting
{
    [TestClass]
    public class OwinApplicationTests
    {
        [TestMethod]
        public void OwinAppTest()
        {
            using (var server = TestServer.Create<MyStartup>())
            {
                HttpResponseMessage response =  server.HttpClient.GetAsync("/Alexa").Result;

                //Execute necessary tests
                Assert.Equals("Hello world using OWIN TestServer",  response.Content.ReadAsStringAsync().Result);
            }
        }
    }

    public class MyStartup
    {
        public void Configuration(IAppBuilder app)
        {
           // app.UseErrorPage(); // See Microsoft.Owin.Diagnostics
            //app.UseWelcomePage("/Welcome"); // See Microsoft.Owin.Diagnostics 
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello world using OWIN TestServer");
            });
        }
    }
}