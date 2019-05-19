using goals_api.Dtos;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace goals_api.IntegrationTests
{
    public class UsersControllerIntegrationTest
    {
        [Fact]
        public async Task Test_Authenticate_Success()
        {
            var client = new TestClientProvider().Client;

            var jsonInString = JsonConvert.SerializeObject(new UserLoginDto
            {
                Username = "kazkas",
                Password = "kazkas"
            });

            var response = await client.PostAsync("/api/user/authenticate", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
