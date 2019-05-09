using goals_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace goals_api.Services
{
    public interface IGoogleApiService
    {
        Task<string> UpdateAllData(User user);
        Task<string> UpdateUserStepsData(User user);
        Task<string> UpdateUserCaloriesData(User user);
    }
}
