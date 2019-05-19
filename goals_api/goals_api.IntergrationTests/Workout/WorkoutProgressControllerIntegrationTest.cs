using goals_api.Dtos.RequestDto;
using goals_api.Dtos.RequestDto.Comment;
using goals_api.Dtos.RequestDto.User;
using goals_api.Dtos.RequestDto.Workouts;
using goals_api.IntergrationTests;
using goals_api.Models.Workouts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace goals_api.IntergrationTests.Workout
{
    public class WorkoutProgressControllerIntegrationTest
    {
        [Fact]
        public async Task WorkoutProgress_Post_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new WorkoutProgressCreateDto
                {
                    Id = -10
                });

                var response = await client.PostAsync("/api/workoutProgress", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            }
        }
        [Fact]
        public async Task WorkoutProgress_Get_Success()
        {
            using (var client = new TestClientProvider().Client)
            {

                var response = await client.GetAsync("/api/workoutProgress");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task WorkoutProgress_UpdateUserGoalProgress_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new WorkoutProgressDto
                {
                    RoutePointId = -11,
                    WorkoutProgress = 1
                });

                var response = await client.PatchAsync("/api/workoutProgress/-10", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}
