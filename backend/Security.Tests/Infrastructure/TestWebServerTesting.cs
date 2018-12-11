using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Security.Tests.Infrastructure
{
    [TestFixture]
    public class TestWebServerTesting
    {
        [Test]
        public void ServerRunTest()
        {
            var webServer = new TestWebServer();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://localhost:14797/api/common/create-token?loginOrEmail=admin&password=admin");
            request.Method = HttpMethod.Post;
            var token = webServer.SendRequest<string>(request);

            Assert.AreEqual(100, token.Length);
        }
    }
}