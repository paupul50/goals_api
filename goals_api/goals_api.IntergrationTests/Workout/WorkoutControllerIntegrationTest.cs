using goals_api.Dtos.RequestDto;
using goals_api.Dtos.RequestDto.Comment;
using goals_api.Dtos.RequestDto.User;
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
    public class WorkoutControllerIntegrationTest
    {
        [Fact]
        public async Task Workout_Post_Created()
        {
            using (var client = new TestClientProvider().Client)
            {
                var routePoints = new List<RoutePoint>
                {
                    new RoutePoint
                    {
                        Lat =  50,
                        Lng = 50,
                        Radius = 50,
                        FillColour = "Black",
                        CircleDraggable = false,
                        Editable = false,
                        Index = 1
                    },
                    new RoutePoint
                    {
                        Lat =  60,
                        Lng = 60,
                        Radius = 20,
                        FillColour = "Black",
                        CircleDraggable = false,
                        Editable = false,
                        Index = 2
                    }
                };
                var jsonInString = JsonConvert.SerializeObject(new WorkoutDto
                {
                    Name = "integration",
                    RoutePoints = routePoints
                });

                var response = await client.PostAsync("/api/workout", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }
        [Fact]
        public async Task Workout_Delete_Unauthorised()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/workout/0");

                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            }
        }

        [Fact]
        public async Task Workout_GetUserWorkouts_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/workout/user");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Workout_GetUserUnusedWorkouts_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/workout/unused");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Workout_GetGroupUnusedWorkouts_NoContent()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/workout/groupUnused");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Workout_GetGroupWorkouts_NoContent()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/workout/group");

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }

        [Fact]
        public async Task Workout_Get_NoContent()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/workout/0");

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}
