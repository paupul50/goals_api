using goals_api.Dtos.RequestDto.Comment;
using goals_api.Dtos.RequestDto.User;
using goals_api.IntergrationTests;
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
    public class GoogleFitControllerIntegrationTest
    {
        [Fact]
        public async Task GoogleFit_Post_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new GoogleAccessDto
                {
                    Token = "InvalidTokenTest"
                });

                var response = await client.PostAsync("/api/googleFit", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GoogleFit_Get_SessionEnd()
        {
            using (var client = new TestClientProvider().Client)
            {

                var response = await client.GetAsync("/api/googleFit");

                var result = await response.Content.ReadAsStringAsync();

                dynamic json = JsonConvert.DeserializeObject(result);

                string resultValue = json.error;

                Assert.Equal("session_end", resultValue);
            }
        }
    }
}
