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
using goals_api.Dtos;
using goals_api.Dtos.RequestDto.Goals;
using Xunit;

namespace goals_api.IntergrationTests.Goal
{
    public class GoalsControllerIntegrationTest
    {
        [Fact]
        public async Task Goals_SaveUserGoal_Conflict()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new GoalDto
                {
                    GoalStringValue = "",
                    GoalNumberValue = 0,
                    GoalType = 101,
                    IsGroupGoal = false,
                    Goalname = "sameName",
                    WorkoutId = 0
                });

                var response = await client.PostAsync("/api/goals/create", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            }
        }

        [Fact]
        public async Task Goals_RemoveUserGoal_Fail()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/goals/0");

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Fact]
        public async Task Goals_GetUserGoal_Fail()
        {
            using (var client = new TestClientProvider().Client)
            {

                var response = await client.GetAsync("/api/goals/0");

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Fact]
        public async Task Goals_GetUserGoalWithProgress_Success()
        {
            using (var client = new TestClientProvider().Client)
            {

                var response = await client.GetAsync("/api/goals/todayProgress");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Goals_GetUserGoalsWithProgress_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new GoalWithProgressDto
                {
                    DateTimeOffset = DateTime.Now.AddDays(-10),
                    DayLimit = 20
                });

                var response = await client.PostAsync("/api/goals/progressHistory", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
