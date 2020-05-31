using JagraTaskManager.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Data
{
    public interface ITeamRepository
    {
        Task<Team> Create(Team team, User user);
        Task<Team> GetById(string teamId);
        Task<IEnumerable<Team>> GetByOrganizationId(string organizationId);
        Task<IEnumerable<Team>> GetByUser(string userId);
        Task<IEnumerable<User>> GetMembers(string teamId);
        Task<Team> AddMember(TeamUser teamUser);
        Task<Team> Delete(Team team);
        Task<Team> Modify(Team team);
        Task<bool> ExistByName(Team team);
        Task<bool> ExistById(string teamId);
    }
}
