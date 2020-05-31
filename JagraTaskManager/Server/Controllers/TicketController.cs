using AutoMapper;
using JagraTaskManager.Server.Data;
using JagraTaskManager.Server.Helpers;
using JagraTaskManager.Server.Models;
using JagraTaskManager.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _repo;
        private readonly IUserRepository _users;
        private readonly IOrganizationRepository _organizations;
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teams;

        public TicketController(ITicketRepository repo, IUserRepository users, IOrganizationRepository organizations, ITeamRepository teams, IMapper mapper)
        {
            _teams = teams;
            _repo = repo;
            _users = users;
            _organizations = organizations;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string ticketId)
        {
            var ticket = await _repo.GetById(ticketId);
            var userId = HttpContext.GetUserId();
            if (!await _organizations.UserInOrganization(userId, ticket.Organization.Id))
            {
                return BadRequest($"User with ID {userId} has no access to Ticket with ID {ticketId}.");
            }
            return Ok(_mapper.Map<TicketForListDto>(ticket));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(TicketForCreationDto ticket)
        {
            ticket.CreatorId = HttpContext.GetUserId();
            if (!await _users.UserExist(ticket.CreatorId)
                || !await _users.UserExist(ticket.AssigneeId)
                || !await _teams.ExistById(ticket.TeamId))
            {
                return BadRequest("Bad request data, data don't exist.");
            }
            var team = await _teams.GetById(ticket.TeamId);
            if (!await _organizations.OrganizationExistById(team.Organization.Id)
                || !await _organizations.UserInOrganization(ticket.CreatorId, team.Organization.Id))
            {
                return BadRequest($"User has no access to Organization with ID {team.Organization.Id}.");
            }
            var taskToCreate = new Ticket
            {
                Title = ticket.Title,
                Description = ticket.Description,
                CreatorId = ticket.CreatorId,
                AssigneeId = ticket.AssigneeId,
                OrganizationId = team.Organization.Id,
                TeamId = team.Id,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
            };
            var taskCreated = await _repo.Create(taskToCreate);
            var taskToReturn = _mapper.Map<TicketForListDto>(taskCreated);
            return Ok(taskToReturn);
        }

        [HttpGet("byorg")]
        public async Task<IActionResult> GetTicketsByOrganization(string organizationId)
        {
            var userId = HttpContext.GetUserId();
            if (!await _organizations.OrganizationExistById(organizationId))
            {
                return BadRequest("Organization does not exist.");
            }
            if (!await _users.UserExist(userId))
            {
                return BadRequest($"User with Id {userId} does not exist.");
            }
            if (!await _organizations.UserInOrganization(userId, organizationId))
            {
                return BadRequest($"User with Id {userId} has no access to Organization with ID {organizationId}.");
            }
            var tickets = await _repo.GetByOrganization(organizationId);
            var ticketsToReturn = _mapper.Map<List<TicketForListDto>>(tickets);
            return Ok(ticketsToReturn);
        }

        [HttpGet("byteam")]
        public async Task<IActionResult> GetTicketsByTeam(string teamId)
        {
            if (!await _teams.ExistById(teamId))
            {
                return BadRequest($"Team with ID {teamId} does not exist.");
            }
            var team = await _teams.GetById(teamId);
            var userId = HttpContext.GetUserId();
            if (!await _organizations.UserInOrganization(userId, team.Organization.Id))
            {
                return BadRequest($"User with ID {userId} has no access to the tickets.");
            }
            var tickets = _mapper.Map<List<TicketForListDto>>(await _repo.GetByTeamId(teamId));
            return Ok(tickets);
        }

        [HttpGet("bycreator")]
        public async Task<IActionResult> GetTicketsByCreator()
        {
            var userId = HttpContext.GetUserId();
            var tickets = await _repo.GetByCreator(userId);
            var ticketsToReturn = _mapper.Map<TicketForListDto>(tickets);
            return Ok(ticketsToReturn);
        }

        [HttpGet("byassignee")]
        public async Task<IActionResult> GetTicketsByAssignee()
        {
            var userId = HttpContext.GetUserId();
            var tickets = await _repo.GetByAssignee(userId);
            var ticketsToReturn = _mapper.Map<TicketForListDto>(tickets);
            return Ok(ticketsToReturn);
        }

        [HttpGet("bywatcher")]
        public async Task<IActionResult> GetTicketsByWatcher()
        {
            var userId = HttpContext.GetUserId();
            var tickets = await _repo.GetByWatcher(userId);
            var ticketsToReturn = _mapper.Map<TicketForListDto>(tickets);
            return Ok(ticketsToReturn);
        }

        [HttpPost("updateTitle")]
        public async Task<IActionResult> UpdateTitle(TicketForListDto ticketForListDto)
        {
            var userId = HttpContext.GetUserId();
            if (!await _organizations.UserInOrganization(userId, ticketForListDto.OrganizationId))
            {
                return BadRequest($"User with ID {userId} has no access to Ticket with ID {ticketForListDto.Id}");
            }
            if (!await _repo.ExistById(ticketForListDto.Id))
            {
                return BadRequest($"Ticket with ID {ticketForListDto.Id} does not exist.");
            }
            var ticket = await _repo.GetById(ticketForListDto.Id);
            ticket.Title = ticketForListDto.Title;
            var ticketToReturn = await _repo.Modify(ticket);
            return Ok(_mapper.Map<TicketForListDto>(ticket));
        }

        [HttpPost("updateDescription")]
        public async Task<IActionResult> UpdateDescription(TicketForListDto ticketForListDto)
        {
            var userId = HttpContext.GetUserId();
            if (!await _organizations.UserInOrganization(userId, ticketForListDto.OrganizationId))
            {
                return BadRequest($"User with ID {userId} has no access to Ticket with ID {ticketForListDto.Id}");
            }
            if (!await _repo.ExistById(ticketForListDto.Id))
            {
                return BadRequest($"Ticket with ID {ticketForListDto.Id} does not exist.");
            }
            var ticket = await _repo.GetById(ticketForListDto.Id);
            ticket.Description = ticketForListDto.Description;
            var ticketToReturn = await _repo.Modify(ticket);
            return Ok(_mapper.Map<TicketForListDto>(ticket));
        }

        [HttpPost("updateAssignee")]
        public async Task<IActionResult> UpdateAssignee(TicketForListDto ticketForListDto)
        {
            var userId = HttpContext.GetUserId();
            if (!await _organizations.UserInOrganization(userId, ticketForListDto.OrganizationId))
            {
                return BadRequest($"User with ID {userId} has no access to Ticket with ID {ticketForListDto.Id}");
            }
            if (!await _repo.ExistById(ticketForListDto.Id))
            {
                return BadRequest($"Ticket with ID {ticketForListDto.Id} does not exist.");
            }
            var ticket = await _repo.GetById(ticketForListDto.Id);
            ticket.AssigneeId = ticketForListDto.Assignee.Id;
            var ticketToReturn = await _repo.Modify(ticket);
            return Ok(_mapper.Map<TicketForListDto>(ticket));
        }
    }
}
