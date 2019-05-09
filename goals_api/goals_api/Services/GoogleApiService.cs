using goals_api.Models;
using goals_api.Models.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public class GoogleApiService : IGoogleApiService
    {

        private readonly DataContext _dataContext;
        private HttpClient _httpClient;

        public GoogleApiService(DataContext dataContext)
        {
            this._dataContext = dataContext;
            this._httpClient = new HttpClient();
        }

        public async Task<string> UpdateAllData(User user)
        {
            var result = await UpdateUserCaloriesData(user);
            if (result == "session_end" || result == "error" || result == "no_data")
            {
                return result;
            }

            result = await UpdateUserStepsData(user);

            if (result == "session_end" || result == "error" || result == "no_data")
            {
                return result;
            }

            return "success";
        }
        

        public async Task<string> UpdateUserStepsData(User user)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.GoogleToken);

            var stepsGoalProgresses = _dataContext.GoalProgresses.Include(gp=>gp.Goal).Where(gp => gp.User == user && gp.Goal.GoalType == 3);

            return await UpdateStepsProgresses(stepsGoalProgresses);
        }

        public async Task<string> UpdateUserCaloriesData(User user)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.GoogleToken);

            var stepsGoalProgresses = _dataContext.GoalProgresses.Include(gp => gp.Goal).Where(gp => gp.User == user && gp.Goal.GoalType == 2);

            return await UpdateCaloriesProgresses(stepsGoalProgresses);
        }
        private async Task<string> UpdateCaloriesProgresses(IQueryable<GoalProgress> goalProgresses)
        {
            try
            {
                var today = DateTime.Now;
                var aggregateBy = new List<object>
                {
                    new
                    {
                        dataTypeName = "com.google.calories.expended",
                        dataSourceId = "derived:com.google.calories.expended:com.google.android.gms:merge_calories_expended"

                    }
                };
                var bucketByTime = new
                {
                    durationMillis = 86400000

                };
                var tempJesus = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                var startTime = (new DateTime(today.Year, today.Month, today.Day - 1, 0, 0, 0) - tempJesus).TotalMilliseconds;
                var endTime = (new DateTime(today.Year, today.Month, today.Day, 23, 59, 59) - tempJesus).TotalMilliseconds;
                var jsonInString = JsonConvert.SerializeObject(new
                {
                    aggregateBy,
                    bucketByTime,
                    startTimeMillis = startTime,
                    endTimeMillis = endTime,
                });

                var uri = "https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate";
                var response = await _httpClient.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return "session_end";
                }

                var result = await response.Content.ReadAsStringAsync();

                dynamic json = JsonConvert.DeserializeObject(result);
                if (json.bucket[0].dataset[0].point == null && json.bucket[1].dataset[0].point == null)
                {
                    return "no_data";
                }
                float stepsFloat = json.bucket[0].dataset[0].point[0].value[0].fpVal;
                int steps = (int)stepsFloat;
                float stepsFloat2 = json.bucket[1].dataset[0].point[0].value[0].fpVal;
                int steps2 = (int)stepsFloat2;
                foreach (var progress in goalProgresses)
                {
                    if(progress.CreatedAt.Day == today.Day)
                    {
                        progress.GoalNumberValue = steps2;
                    }
                    else
                    {
                        progress.GoalNumberValue = steps;
                    }

                    progress.IsDone = progress.GoalNumberValue >= progress.Goal.GoalNumberValue ? true : false;
                }

                _dataContext.GoalProgresses.UpdateRange(goalProgresses);
                _dataContext.SaveChanges();

                return "success";
            }
            catch (Exception exe)
            {
                return "error";
            }
        }

        private async Task<string> UpdateStepsProgresses(IQueryable<GoalProgress> goalProgresses)
        {
            try
            {
                var today = DateTime.Now;
                var aggregateBy = new List<object>
                {
                    new
                    {
                        dataTypeName = "com.google.step_count.delta",
                        dataSourceId = "derived:com.google.step_count.delta:com.google.android.gms:estimated_steps"

                    }
                };
                var bucketByTime = new
                {
                    durationMillis = 86400000

                };
                var tempJesus = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                var startTime = (new DateTime(today.Year, today.Month, today.Day - 1, 0, 0, 0) - tempJesus).TotalMilliseconds;
                var endTime = (new DateTime(today.Year, today.Month, today.Day, 23, 59, 59) - tempJesus).TotalMilliseconds;
                var jsonInString = JsonConvert.SerializeObject(new
                {
                    aggregateBy,
                    bucketByTime,
                    startTimeMillis = startTime,
                    endTimeMillis = endTime,
                });

                var uri = "https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate";
                var response = await _httpClient.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return "session_end";
                }

                var result = await response.Content.ReadAsStringAsync();

                dynamic json = JsonConvert.DeserializeObject(result);
                if (json.bucket[0].dataset[0].point == null && json.bucket[1].dataset[0].point == null)
                {
                    return "no_data";
                }
                int colories = json.bucket[0].dataset[0].point[0].value[0].intVal;
                int colories2 = json.bucket[1].dataset[0].point[0].value[0].intVal;
                foreach (var progress in goalProgresses)
                {
                    if (progress.CreatedAt.Day == today.Day)
                    {
                        progress.GoalNumberValue = colories2;
                    }
                    else
                    {
                        progress.GoalNumberValue = colories;
                    }

                    progress.IsDone = progress.GoalNumberValue >= progress.Goal.GoalNumberValue ? true : false;
                }

                _dataContext.GoalProgresses.UpdateRange(goalProgresses);
                _dataContext.SaveChanges();

                return "success";
            }
            catch (Exception)
            {
                return "error";
            }
        }
    }
}
