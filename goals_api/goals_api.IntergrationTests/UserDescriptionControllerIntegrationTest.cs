using goals_api.Dtos.RequestDto.User;
using goals_api.IntergrationTests;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace goals_api.IntergrationTests
{
    public class UserDescriptionControllerIntegrationTest
    {

        [Fact]
        public async Task UserDescription_GetUserDescription_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new UserDescriptionDto
                {
                    Username = "integration"
                });

                var response = await client.PostAsync("/api/userDescription/other", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task UserDescription_GetCurrentUserDescription_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/userDescription");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
