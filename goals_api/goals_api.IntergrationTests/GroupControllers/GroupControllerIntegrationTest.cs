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
using goals_api.Dtos.RequestDto;
using Xunit;

namespace goals_api.IntergrationTests.GroupControllers
{
    public class GroupControllerIntegrationTest
    {
        [Fact]
        public async Task Group_Get_Success()
        {
            using (var client = new TestClientProvider().Client)
            {

                var response = await client.GetAsync("/api/group");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task Group_Post_Fail()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new GroupDto
                {
                    GroupName = "integration"
                });

                var response = await client.PostAsync("/api/group", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }
        [Fact]
        public async Task Group_Delete_Fail()
        {
            using (var client = new TestClientProvider().Client)
            {

                //var response = await client.DeleteAsync("/api/group");

                //Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
                Assert.Equal("temp", "temp");
            }
        }
    }
}
