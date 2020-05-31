using AutoMapper;
using IdentityModel;
using JagraTaskManager.Server.Data;
using JagraTaskManager.Server.Helpers;
using JagraTaskManager.Server.Models;
using JagraTaskManager.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _repo;
        private readonly IInvitationRepository _invitations;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _users;
        private readonly ITicketRepository _tickets;
        private readonly IMapper _mapper;
        public OrganizationController(
            IOrganizationRepository repo,
            IInvitationRepository invitations,
            UserManager<User> userManager,
            IUserRepository users,
            ITicketRepository tickets,
            IConfiguration config,
            IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _users = users;
            _tickets = tickets;
            _config = config;
            _repo = repo;
            _invitations = invitations;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] OrganizationForCreationDto orgDto)
        {
            var userId = HttpContext.GetUserId();
            if (await _repo.OrganizationExistByName(orgDto.Name) || !await _users.UserExist(userId))
            {
                return BadRequest("Organization name already exist");
            }
            Organization org = new Organization
            {
                Name = orgDto.Name
            };
            User user = await _users.GetUser(userId);
            var orgCreated = await _repo.Create(org, user);
            return Ok(orgCreated);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganization(string organizationId)
        {
            var userId = HttpContext.GetUserId();
            if (!await _users.UserExist(userId))
            {
                return BadRequest($"User with Id {userId} does not exist.");
            }
            if (!await _repo.OrganizationExistById(organizationId))
            {
                return BadRequest($"Organization with ID {organizationId} does not exist.");
            }
            if (!await _repo.UserInOrganization(userId, organizationId))
            {
                return BadRequest($"User with Id {userId} has no access to Organization with ID {organizationId}.");
            }
            var org = await _repo.GetOrganization(organizationId);
            var orgToReturn = _mapper.Map<OrganizationForListDto>(org);
            return Ok(orgToReturn);
        }

        [HttpGet("byuser")]
        public async Task<IActionResult> GetOrganizationsByUser()
        {
            var userId = HttpContext.GetUserId();
            if (!await _users.UserExist(userId))
            {
                return BadRequest($"User with Id {userId} does not exist.");
            }
            var orgs = await _repo.GetOrganizationsByUser(userId);
            var orgsToReturn = _mapper.Map<List<OrganizationForListDto>>(orgs);

            return Ok(orgsToReturn);
        }

        [HttpPost("color")]
        public async Task<IActionResult> ChangeOrganizationColor(string id, string color)
        {
            if (!await _repo.OrganizationExistById(id))
            {
                return BadRequest("Organization does not exist.");
            }
            var org = await _repo.GetOrganization(id);
            org.Color = color;
            await _repo.UpdateOrganization(org);
            return Ok(org);
        }
    }
}
