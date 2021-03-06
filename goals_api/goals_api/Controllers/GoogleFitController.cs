﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using goals_api.Dtos.RequestDto;
using goals_api.Dtos.RequestDto.User;
using goals_api.Models.DataContext;
using goals_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace goals_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GoogleFitController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IGoogleApiService _googleApiService;

        public GoogleFitController(DataContext dataContext, IGoogleApiService googleApiService)
        {
            this._dataContext = dataContext;
            this._googleApiService = googleApiService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GoogleAccessDto googleAccessDto)
        {
            try
            {
                var currentUser = await _dataContext.Users.FindAsync(User.Identity.Name);
                currentUser.GoogleToken = googleAccessDto.Token;
                _dataContext.Users.Update(currentUser);
                _dataContext.SaveChanges();
                return Ok();
            }
            catch (Exception )
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);

                var result = await _googleApiService.UpdateAllData(currentUser);

                if (result != "success")
                {
                    return Ok(new { error = result });
                }

                return Ok(new { });
            }
            catch (Exception )
            {
                return StatusCode(400);
            }
        }
    }
}