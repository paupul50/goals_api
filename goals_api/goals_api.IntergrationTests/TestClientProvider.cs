using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace goals_api.IntergrationTests
{
    public class TestClientProvider : IDisposable
    {
        private TestServer Server;
        public HttpClient Client { get; private set; }
        private const string integrationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImludGVncmF0aW9uIiwibmJmIjoxNTU4MjkzNTUxLCJleHAiOjE1NTg4OTgzNTEsImlhdCI6MTU1ODI5MzU1MX0.FyC7QHV95BrJrzeGfiaiTPfbNBJiv6lkFf_qGBkU8bw";

        public TestClientProvider()
        {
            Server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = Server.CreateClient();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", integrationToken);
        }

        public void Dispose()
        {
            Server?.Dispose();
            Client?.Dispose();
        }
    }
}
