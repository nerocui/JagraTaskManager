using JagraTaskManager.Client.Services;
using JagraTaskManager.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JagraTaskManager.Client.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly string _rootUrl = "api/team";
        private readonly IHttpService _httpService;

        public TeamRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<TeamForListDto> CreateTeam(TeamForCreationDto team)
        {
            var response = await _httpService.Post<TeamForCreationDto, TeamForListDto>($"{_rootUrl}/create", team);

            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new Exception($"Failed to Create Team. {response.HttpResponseMessage}");
            }
        }

        public async Task<List<TeamForListDto>> GetByOrganization(string organizationId)
        {
            var response = await _httpService.Get<List<TeamForListDto>>($"{_rootUrl}/byorg?orgId={organizationId}");
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new Exception($"Failed to get list of teams. {response.HttpResponseMessage}");
            }
        }

        public async Task<List<TeamForListDto>> GetByUser()
        {
            var response = await _httpService.Get<List<TeamForListDto>>($"{_rootUrl}/byuser");
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new Exception($"Failed to get list of teams. {response.HttpResponseMessage}");
            }
        }

        public async Task<TeamForListDto> GetTeam(string teamId)
        {
            var response = await _httpService.Get<TeamForListDto>($"{_rootUrl}?teamId={teamId}");
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new Exception($"Failed to get list of teams. {response.HttpResponseMessage}");
            }
        }
    }
}