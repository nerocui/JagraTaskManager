using AutoMapper;
using JagraTaskManager.Server.Data;
using JagraTaskManager.Server.Helpers;
using JagraTaskManager.Server.Models;
using JagraTaskManager.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _repo;
        private readonly IUserRepository _users;
        private readonly IOrganizationRepository _orgs;
        private readonly IMapper _mapper;

        public TeamController(ITeamRepository repo, IUserRepository users, IOrganizationRepository orgs, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            _users = users;
            _orgs = orgs;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string teamId)
        {
            var userId = HttpContext.GetUserId();
            if (!await _repo.ExistById(teamId))
            {
                return BadRequest($"Team with ID {teamId} does not exist.");
            }
            var team = await _repo.GetById(teamId);
            if (!await _orgs.UserInOrganization(userId, team.Organization.Id))
            {
                return BadRequest($"User with ID {userId} has no access to Team with ID {teamId}");
            }
            return Ok(_mapper.Map<TeamForListDto>(team));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(TeamForCreationDto teamDto)
        {
            var userId = HttpContext.GetUserId();
            if (!await _orgs.OrganizationExistById(teamDto.OrganizationId))
            {
                return BadRequest($"Organization with ID {teamDto.OrganizationId} does not exist.");
            }
            if (!await _users.UserExist(userId))
            {
                return BadRequest($"User with Id {userId} does not exist.");
            }
            if (!await _orgs.UserInOrganization(userId, teamDto.OrganizationId))
            {
                return BadRequest($"User with Id {userId} has no access to Organization with ID {teamDto.OrganizationId}.");
            }
            var user = await _users.GetUser(userId);
            var organization = await _orgs.GetOrganization(teamDto.OrganizationId);
            var team = new Team
            {
                Name = teamDto.Name,
                OrganizationId = teamDto.OrganizationId,
                Organization = organization
            };
            if (await _repo.ExistByName(team))
            {
                return BadRequest($"Team with name {team.Name} already exist.");
            }
            var teamToReturn = await _repo.Create(team, user);
            return Ok(_mapper.Map<TeamForListDto>(teamToReturn));
        }

        [HttpGet("byorg")]
        public async Task<IActionResult> GetByOrg(string orgId)
        {
            var userId = HttpContext.GetUserId();
            if (!await _orgs.OrganizationExistById(orgId))
            {
                return BadRequest($"Organization with ID {orgId} does not exist.");
            }
            if (!await _users.UserExist(userId))
            {
                return BadRequest($"User with Id {userId} does not exist.");
            }
            if (!await _orgs.UserInOrganization(userId, orgId))
            {
                return BadRequest($"User with Id {userId} has no access to Organization with ID {orgId}.");
            }
            var teams = await _repo.GetByOrganizationId(orgId);
            return Ok(teams);
        }

        [HttpGet("byuser")]
        public async Task<IActionResult> GetByUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claims = identity.Claims;
            var identityClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (!await _users.UserExist(identityClaim.Value))
            {
                return BadRequest($"User with Id {identityClaim.Value} does not exist.");
            }
            var teams = await _repo.GetByUser(identityClaim.Value);
            return Ok(_mapper.Map<List<TeamForListDto>>(teams));
        }
    }
}
