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
    public class StatisticsControllerIntergrationTest
    {
        [Fact]
        public async Task Statistics_GetChart_Success()
        {
            using (var client = new TestClientProvider().Client)
            {

                var response = await client.GetAsync("/api/statistics/chart");

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Statistics_GetLeaderBoard_Success()
        {
            using (var client = new TestClientProvider().Client)
            {

                var response = await client.GetAsync("/api/statistics/leaderboard");

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }
        }
    }
}
