using goals_api.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace goals_api.Services
{
    public class GoogleApiService: IGoogleApiService
    {
        public async Task<string> GetUserData(User user)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.GoogleToken);

            var aggregateBy = new List<object>();
            aggregateBy.Add(new
            {
                dataTypeName = "com.google.step_count.delta",
                dataSourceId = "derived:com.google.step_count.delta:com.google.android.gms:estimated_steps"

            });
            var bucketByTime = new
            {
                durationMillis = 86400000

            };
            var jsonInString = JsonConvert.SerializeObject(new
            {
                aggregateBy,
                bucketByTime,
                startTimeMillis = 1557090000000,
                endTimeMillis = 1557176340000
            });

            var uri = "https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate";
            var response = await client.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));

            if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return "session_end";
            } else
            {
                var result = await response.Content.ReadAsStringAsync();
                return result.ToString();
            }

            
        }
    }
}
