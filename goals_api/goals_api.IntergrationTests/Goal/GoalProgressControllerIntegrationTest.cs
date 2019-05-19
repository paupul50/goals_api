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
using goals_api.Dtos.RequestDto.GoalProgress;
using Xunit;

namespace goals_api.IntergrationTests.Goal
{
    public class GoalProgressControllerIntegrationTest
    {
        [Fact]
        public async Task GoalProgress_UpdateUserGoalProgress_Fail()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new GoalProgressPatchDto
                {
                    Id = -10,
                    GoalNumberValue = 10,
                    GoalStringValue = "as",
                    IsDone =  true,
                    IsGroup = true
                });

                var response = await client.PostAsync("/api/comment", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}
