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
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace OwinApplicationTesting
{
    [TestClass]
    public class SelfHostingTest
    {
        protected TestServer server;

        [TestInitialize]
        public void Setup()
        {
            server = TestServer.Create(app =>
            {
                HttpConfiguration config = new HttpConfiguration();
                WebApp.WebApiConfig.Register(config);
                app.UseWebApi(config);
            });
        }

        [TestCleanup]
        public void TearDown()
        {
            if (server != null)
                server.Dispose();
        }

        [TestMethod]
        public async Task TestODataMetaData()
        {
            HttpResponseMessage response = await server.CreateRequest("/odata/?$metadata").GetAsync();

            // var result = await response.Content.ReadAsAsync<ODataMetaData>();

            //  Assert.IsTrue(result.value.Count > 0, "Unable to obtain meta data");
        }
    }
}