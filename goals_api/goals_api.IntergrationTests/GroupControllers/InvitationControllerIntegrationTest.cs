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
using goals_api.Dtos.RequestDto.Group.Invitation;
using Xunit;

namespace goals_api.IntergrationTests.GroupControllers
{
    public class InvitationControllerIntegrationTest
    {
        [Fact]
        public async Task Invitation_AcceptInvitation_Fail()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new InvitationAcceptDto
                {
                    LeaderUsername = "integration"
                });

                var response = await client.PostAsync("/api/invitation/accept", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        [Fact]
        public async Task Invitation_Post_Fail()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new InvitationDto
                {
                    MemberUsername = "naujas"
                });

                var response = await client.PostAsync("/api/invitation", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        [Fact]
        public async Task Invitation_Get_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new UserDescriptionDto
                {
                    Username = "integration"
                });

                var response = await client.GetAsync("/api/invitation");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Invitation_Delete_Fail()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/invitation/0");

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }
    }
}
