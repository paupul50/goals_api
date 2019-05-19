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

namespace goals_api.IntergrationTests
{
    public class CommentControllerIntegrationTest
    {
        [Fact]
        public async Task Comment_Post_Success()
        {
            using (var client = new TestClientProvider().Client)
            {
                var jsonInString = JsonConvert.SerializeObject(new CommentDto
                {
                    Body = "Komentaras",
                    CommentTarget = "profile",
                    CommentedUser = "integration"
                });

                var response = await client.PostAsync("/api/comment", new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
