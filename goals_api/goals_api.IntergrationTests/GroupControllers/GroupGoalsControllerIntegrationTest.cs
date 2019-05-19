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


namespace goals_api.IntergrationTests.GroupControllers
{
    public class GroupGoalsControllerIntegrationTest
    {
        [Fact]
        public async Task GroupGoals_RemoveGroupGoal_Fail()
        {
            using (var client = new TestClientProvider().Client)
            {

                var response = await client.DeleteAsync("/api/groupGoals/0");

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Fact]
        public async Task GroupGoals_GetGroupGoal_Fail()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/groupGoals/0");

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }
    }
}
