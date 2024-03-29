﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos;
using goals_api.Dtos.RequestDto.GroupGoals;
using goals_api.Models;
using goals_api.Models.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace goals_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupGoalsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public GroupGoalsController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpDelete("{id:int}")]
        public IActionResult RemoveGroupGoal(int id)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var currentGroup = _dataContext.Groups.SingleOrDefault(g => g.LeaderUsername == currentUser.Username); //Include(group=>group.Members).

                if (currentGroup == null)
                {
                    return StatusCode(401);
                }

                var groupGoal = _dataContext.Goals.Include(g=>g.GoalMedium).SingleOrDefault(g=>g.Id == id);

                if (groupGoal == null || groupGoal.GoalMedium.Group != currentGroup)
                {
                    return StatusCode(401);
                }

                var groupGoalProgresses = _dataContext.GoalProgresses.Where(progress => progress.Goal == groupGoal);
                _dataContext.GoalProgresses.RemoveRange(groupGoalProgresses);
                _dataContext.Goals.Remove(groupGoal);
                _dataContext.SaveChanges();

                return StatusCode(204);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetGroupGoal(int id)
        {
            try
            {
                var currentUser = _dataContext.Users.Find(User.Identity.Name);
                var groupGoal = _dataContext.Goals.Include(gg => gg.GoalMedium.Group).SingleOrDefault(gg => gg.Id == id);
                if (groupGoal == null)
                {
                    return StatusCode(401);
                }

                if (!groupGoal.GoalMedium.Group.Members.Contains(currentUser) && groupGoal.GoalMedium.Group.LeaderUsername != currentUser.Username)
                {
                    return StatusCode(401);
                }

                return Ok(groupGoal);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

    }
}