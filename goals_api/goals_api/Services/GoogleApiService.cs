﻿using goals_api.Models;
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
using Newtonsoft.Json.Linq;

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
            if (isRequestNotSuccessful(result))
            {
                return result;
            }

            result = await UpdateUserStepsData(user);

            if (isRequestNotSuccessful(result))
            {
                return result;
            }

            return "success";
        }

        private bool isRequestNotSuccessful(string result)
        {
            if (result == "session_end" || result == "error")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<string> UpdateUserStepsData(User user)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.GoogleToken);

            var stepsGoalProgresses = _dataContext.GoalProgresses.Include(gp => gp.Goal).Where(gp => gp.User == user && gp.Goal.GoalType == 3);

            return await UpdateStepsProgresses(stepsGoalProgresses);
        }

        public async Task<string> UpdateUserCaloriesData(User user)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.GoogleToken);

            var caloriesGoalProgresses = _dataContext.GoalProgresses.Include(gp => gp.Goal).Where(gp => gp.User == user && gp.Goal.GoalType == 2);

            return await UpdateCaloriesProgresses(caloriesGoalProgresses);
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
                var response = await PostGoogle(today, aggregateBy);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return "session_end";
                }

                var result = await response.Content.ReadAsStringAsync();

                dynamic json = JsonConvert.DeserializeObject(result);

                //if (json.bucket[0].dataset[0].point as JObject == null && json.bucket[1].dataset[0].point as JObject == null)
                //{
                //    return "no_data";
                //}
                string value1 = json.bucket[0].dataset[0].point.ToString();
                string value2 = json.bucket[1].dataset[0].point.ToString();
                if (value1 == "[]" && value2=="[]")
                {
                    return "no_data";
                }

                int steps = 0;
                
                if (value1 != "[]")
                {
                    float stepsFloat = json.bucket[0].dataset[0].point[0].value[0].fpVal;
                    steps = (int)stepsFloat;
                }

                int steps2 = 0;

                if (value2 != "[]")
                {
                    float stepsFloat2 = json.bucket[1].dataset[0].point[0].value[0].fpVal;
                    steps2 = (int)stepsFloat2;
                }


                foreach (var progress in goalProgresses)
                {
                    if (progress.CreatedAt.Day == today.Day)
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
            catch (Exception)
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

                var response = await PostGoogle(today, aggregateBy);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return "session_end";
                }

                var result = await response.Content.ReadAsStringAsync();

                dynamic json = JsonConvert.DeserializeObject(result);

                string value1 = json.bucket[0].dataset[0].point.ToString();
                string value2 = json.bucket[1].dataset[0].point.ToString();
                if (value1 == "[]" && value2 == "[]")
                {
                    return "no_data";
                }

                int calories = 0;
                int calories2 = 0;

                if (value1 != "[]")
                {
                    calories = json.bucket[0].dataset[0].point[0].value[0].intVal;
                }

                if (value2 != "[]")
                {
                    calories2 = json.bucket[1].dataset[0].point[0].value[0].intVal;
                }

                foreach (var progress in goalProgresses)
                {
                    if (progress.CreatedAt.Day == today.Day)
                    {
                        progress.GoalNumberValue = calories2;
                    }
                    else
                    {
                        progress.GoalNumberValue = calories;
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

        private async Task<HttpResponseMessage> PostGoogle(DateTime dateTime, List<dynamic> aggregateBy)
        {
            var bucketByTime = new
            {
                durationMillis = 86400000

            };
            var tempJesus = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var startTime = (new DateTime(dateTime.Year, dateTime.Month, dateTime.Day-2, 21, 0, 0) - tempJesus).TotalMilliseconds;
            var endTime = (new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 21, 0, 0) - tempJesus).TotalMilliseconds;
            var jsonInString = JsonConvert.SerializeObject(new
            {
                aggregateBy,
                bucketByTime,
                startTimeMillis = startTime,
                endTimeMillis = endTime,
            });

            var uri = "https://www.googleapis.com/fitness/v1/users/me/dataset:aggregate";
            return await _httpClient.PostAsync(uri, new StringContent(jsonInString, Encoding.UTF8, "application/json"));
        }
    }
}
