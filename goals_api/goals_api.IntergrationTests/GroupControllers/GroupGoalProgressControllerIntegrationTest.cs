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
    public class GroupGoalProgressControllerIntegrationTest
    {
        [Fact]
        public async Task GroupGoal_GetUserGoalWithProgress_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/groupGoalProgress");

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
        [Fact]
        public async Task GroupGoal_GetUserGoalsWithProgress_Success()
        {
            using (var client = new TestClientProvider().Client)
            {

                var jsonInString = JsonConvert.SerializeObject(new GroupProgressDto
                {
                    GroupProgressDate = DateTime.Now
                });

                var response = await client.PostAsync("/api/groupGoalProgress", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
