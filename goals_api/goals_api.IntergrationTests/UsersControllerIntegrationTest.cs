using goals_api.Dtos;
using goals_api.Dtos.User;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace goals_api.IntergrationTests
{
    public class UsersControllerIntegrationTest
    {

        [Fact]
        public async Task Users_Create_CreateExists()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new UserCreateDto
                {
                    Username = "integration",
                    Password = "integration",
                    Firstname = "integration",
                    Lastname = "integration",
                    Email = "integration@gmail.com"
                });

                var response = await client.PostAsync("/api/users/create", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            }
        }
        [Fact]
        public async Task Users_Authenticate_BadInput()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new UserLoginDto
                {
                    Username = "integration",
                    Password = "qweqweqwe"
                });

                var response = await client.PostAsync("/api/users/authenticate", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }

        }
    }
}
