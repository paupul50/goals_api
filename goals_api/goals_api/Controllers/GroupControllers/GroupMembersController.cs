﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos.RequestDto.Group;
using goals_api.Models.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace goals_api.Controllers.GroupControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupMembersController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public GroupMembersController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpPost("specific")]
        public IActionResult Delete(MemberDeleteDto memberDeleteDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var userGroup = _dataContext.Groups.SingleOrDefault(group => group.LeaderUsername == currentUser.Username);
                var userToDelete = _dataContext.Users.First(user => user.Username == memberDeleteDto.MemberUsername);
                if (userGroup == null)
                {
                    return StatusCode(401);
                }
                var groupGoals = _dataContext.GroupGoals.Where(g => g.Group == userGroup);
                foreach (var goal in groupGoals)
                {
                    var groupGoalProgress = _dataContext.GroupGoalProgresses.Where(ggp => ggp.Goal == goal
                    && ggp.MemberUsername == memberDeleteDto.MemberUsername);
                    _dataContext.GroupGoalProgresses.RemoveRange(groupGoalProgress);
                }
                userGroup.Members.Remove(userToDelete);

                _dataContext.Groups.Update(userGroup);
                _dataContext.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var userGroup = _dataContext.Groups.SingleOrDefault(group => group.Members.Contains(currentUser));
                if (userGroup == null)
                {
                    return StatusCode(401);
                }
                var groupGoals = _dataContext.GroupGoals.Where(g => g.Group == userGroup);
                foreach (var goal in groupGoals)
                {
                    var groupGoalProgress = _dataContext.GroupGoalProgresses.Where(ggp => ggp.Goal == goal
                    && ggp.MemberUsername == currentUser.Username);
                    _dataContext.GroupGoalProgresses.RemoveRange(groupGoalProgress);
                }

                userGroup.Members.Remove(currentUser);

                _dataContext.Groups.Update(userGroup);
                _dataContext.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var currentUserGroup = _dataContext.Groups.Include(group=>group.Members).SingleOrDefault(g => g.LeaderUsername == currentUser.Username);
                if (currentUserGroup == null)
                {
                    return StatusCode(401);
                }
                var members = currentUserGroup.Members.Select(member=>
                new
                {
                    member.FirstName,
                    member.LastName,
                    member.Username
                }
                );

                return Ok(members);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}