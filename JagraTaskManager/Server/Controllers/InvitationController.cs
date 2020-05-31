using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JagraTaskManager.Server.Data;
using JagraTaskManager.Server.Helpers;
using JagraTaskManager.Server.Models;
using JagraTaskManager.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JagraTaskManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationRepository _repo;
        private readonly IOrganizationRepository _orgs;
        private readonly IMapper _mapper;
        private readonly IUserRepository _users;
        public InvitationController(IInvitationRepository repo, IUserRepository users, IOrganizationRepository orgs, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
            _orgs = orgs;
            _repo = repo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> InviteUser(InvitationForCreationDto invitationForCreation)
        {
            var inviterId = HttpContext.GetUserId();
            var organizationId = invitationForCreation.OrganizationId;
            if (!await _orgs.OrganizationExistById(invitationForCreation.OrganizationId))
            {
                return BadRequest($"Organization with ID {organizationId} does not exist.");
            }
            if (!await _orgs.UserInOrganization(inviterId, organizationId))
            {
                return BadRequest($"User with ID {inviterId} has no access to Organization with ID {organizationId}.");
            }
            var inviter = await _users.GetUser(inviterId);
            if (!await _users.UserExist(invitationForCreation.UserId))
            {
                return BadRequest($"User with ID {invitationForCreation.UserId} does not exist.");
            }
            var user = await _users.GetUser(invitationForCreation.UserId);
            var organization = await _orgs.GetOrganization(organizationId);
            if (!await _orgs.IsAdmin(organization, inviter))
            {
                return BadRequest("Not Authorized To Invite New Member");
            }
            if (await _repo.InvitationExist(organizationId, user.Id))
            {
                return Conflict("Invitation Already Exist");
            }
            Invitation invitation = new Invitation
            {
                UserId = user.Id,
                OrganizationId = organizationId
            };
            await _repo.Add(invitation);
            //TODO, return list of all invitations that belong to this organization
            var invitations = await _repo.GetInvitationsByOrganization(organization.Id);
            return Ok(_mapper.Map<List<InvitationForListDto>>(invitations));
        }

        [HttpGet("byOrg")]
        public async Task<IActionResult> GetByOrganization(string organizationId)
        {
            var userId = HttpContext.GetUserId();
            if (!await _users.UserExist(userId))
            {
                return BadRequest($"User with ID {userId} does not exist.");
            }
            if (!await _orgs.OrganizationExistById(organizationId))
            {
                return BadRequest($"Organization with ID {organizationId} does not exist.");
            }
            if (!await _orgs.UserInOrganization(userId, organizationId))
            {
                return BadRequest($"User with ID {userId} has no access to Organization with ID {organizationId}.");
            }
            var user = await _users.GetUser(userId);
            
            var invitations = await _repo.GetInvitationsByOrganization(organizationId);
            return Ok(_mapper.Map<InvitationForListDto>(invitations));
        }

        [HttpGet("byUser")]
        public async Task<IActionResult> GetByUser()
        {
            var userId = HttpContext.GetUserId();
            if (!await _users.UserExist(userId))
            {
                return BadRequest($"User with ID {userId} does not exist.");
            }
            var invitations = await _repo.GetInvitationsByUser(userId);
            Console.WriteLine(invitations);
            var invitationsToReturn = _mapper.Map<List<InvitationForListDto>>(invitations);
            return Ok(invitationsToReturn);
        }
    }
}