using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JagraTaskManager.Server.Data;
using JagraTaskManager.Server.Helpers;
using JagraTaskManager.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JagraTaskManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IOrganizationRepository _organizations;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repo, IOrganizationRepository organizations, IMapper mapper)
        {
            _repo = repo;
            _organizations = organizations;
            _mapper = mapper;
        }

        [HttpGet("byorg")]
        public async Task<IActionResult> GetByOrg(string orgId)
        {
            var userId = HttpContext.GetUserId();
            if (!await _organizations.UserInOrganization(userId, orgId))
            {
                return BadRequest($"User with ID {userId} has no access to Organization with ID {orgId}.");
            }
            var users = await _repo.GetUsersByOrganization(orgId);
            return Ok(_mapper.Map<List<UserForListDto>>(users));
        }

        [HttpGet("searchByEmail")]
        public async Task<IActionResult> SearchByEmail(string email)
        {
            if (!await _repo.UserExistByEmail(email))
            {
                return BadRequest($"User with Email {email} does not exist.");
            }
            var user = await _repo.GetUserByEmail(email);
            return Ok(_mapper.Map<UserForListDto>(user));
        }
    }
}
