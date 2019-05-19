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
using goals_api.Dtos.RequestDto.Group;
using Xunit;
namespace goals_api.IntergrationTests.GroupControllers
{
    public class GroupMembersControllerIntegrationTest
    {
        [Fact]
        public async Task GroupMembers_DeleteSpecific_Fail()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new MemberDeleteDto
                {
                    MemberUsername = "aaaa"
                });

                var response = await client.PostAsync("/api/groupMembers/specific", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }
        [Fact]
        public async Task GroupMembers_Delete_Fail()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/groupMembers");

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }
        [Fact]
        public async Task GroupMembers_Get_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/groupMembers");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
