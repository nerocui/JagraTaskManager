using JagraTaskManager.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Client.Repositories
{
    public interface ITeamRepository
    {
        Task<List<TeamForListDto>> GetByUser();
        Task<List<TeamForListDto>> GetByOrganization(string organizationId);
        Task<TeamForListDto> GetTeam(string teamId);
        Task<TeamForListDto> CreateTeam(TeamForCreationDto teamForCreationDto);
    }
}