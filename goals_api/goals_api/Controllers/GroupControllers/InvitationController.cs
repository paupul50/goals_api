using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using goals_api.Dtos.RequestDto.Group.Invitation;
using goals_api.Models;
using goals_api.Models.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace goals_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvitationController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public InvitationController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        [HttpPost("accept")]
        public IActionResult AcceptInvitation([FromBody] InvitationAcceptDto invitationAcceptDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var invitation = _dataContext.GroupInvitations.SingleOrDefault(i => i.LeaderUsername == invitationAcceptDto.LeaderUsername
                && i.MemberUsername == currentUser.Username);
                if (invitation==null)
                {
                    return NoContent();
                }

                var group = _dataContext.Groups.SingleOrDefault(g => g.LeaderUsername == invitation.LeaderUsername);
                if(group==null)
                {
                    _dataContext.GroupInvitations.Remove(invitation);

                    _dataContext.SaveChanges();

                    return NoContent();
                }
                group.Members.Add(currentUser);
                _dataContext.GroupInvitations.Remove(invitation);
                _dataContext.Groups.Update(group);

                _dataContext.SaveChanges();

                return StatusCode(201);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] InvitationDto invitationDto)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var currentUserGroup = _dataContext.Groups.SingleOrDefault(g => g.LeaderUsername == currentUser.Username);
                var invitedUser = _dataContext.Users.Find(invitationDto.MemberUsername);
                var invitedUserInvitation = _dataContext.GroupInvitations
                    .SingleOrDefault(gi => gi.LeaderUsername == currentUser.Username && gi.MemberUsername== invitationDto.MemberUsername);
                if (invitedUser == currentUser || invitedUser == null || currentUserGroup == null || invitedUserInvitation != null)
                {
                    return StatusCode(400);
                }
                if (IsUserInGroup(invitedUser))
                {
                    return StatusCode(409);
                }
                var newInvitation = new GroupInvitation
                {
                    CreateAt = DateTime.Now,
                    LeaderUsername = currentUser.Username,
                    MemberUsername = invitedUser.Username
                };
                _dataContext.GroupInvitations.Add(newInvitation);
                _dataContext.SaveChanges();

                // return Ok(newInvitation);
                return StatusCode(204);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        private bool IsUserInGroup(User invitedUser)
        {
            var userGroup = _dataContext.Groups.SingleOrDefault(group => group.Members.Contains(invitedUser)); // .Include(group=>group.Members)
            if (userGroup == null)
            {
                var groupInLead = _dataContext.Groups.SingleOrDefault(group => group.LeaderUsername == invitedUser.Username);
                if (groupInLead == null)
                {
                    return false;
                }
                return true;
            }
            return true;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var currentUserGroup = _dataContext.Groups.SingleOrDefault(g => g.LeaderUsername == currentUser.Username);
                if(currentUserGroup == null)
                {
                    var userInvitations = _dataContext.GroupInvitations.Where(gi => gi.MemberUsername == currentUser.Username);

                    return Ok(userInvitations);
                }
                var userSentInvitations = _dataContext.GroupInvitations.Where(gi => gi.LeaderUsername == currentUser.Username);

                return Ok(userSentInvitations);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var currentUser = _dataContext.Users.Find(User.Identity.Name);
            try
            {
                var groupInvitation = _dataContext.GroupInvitations.Find(id);
                if(groupInvitation == null)
                {
                    return StatusCode(401);
                }
                if(groupInvitation.LeaderUsername == currentUser.Username 
                    || groupInvitation.MemberUsername == currentUser.Username)
                {
                    _dataContext.GroupInvitations.Remove(groupInvitation);
                    _dataContext.SaveChanges();
                    return StatusCode(204);
                }
                return StatusCode(401);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}