using JagraTaskManager.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Data
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;
        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Team> AddMember(TeamUser teamUser)
        {
            await _context.TeamUsers.AddAsync(teamUser);
            await _context.SaveChangesAsync();
            return await GetById(teamUser.TeamId);
        }

        public async Task<Team> Create(Team team, User user)
        {
            var tracker = await _context.Teams.AddAsync(team);
            await AddMember(new TeamUser { TeamId = tracker.Entity.Id, UserId = user.Id, Role = "Leader" });
            await _context.SaveChangesAsync();
            var teamToReturn = tracker.Entity;
            teamToReturn.Leader = user;
            return teamToReturn;
        }

        public Task<Team> Delete(Team team)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistById(string teamId)
        {
            return await _context.Teams
                .AnyAsync(
                    t => t.Id == teamId
                );
        }

        public async Task<bool> ExistByName(Team team)
        {
            return await _context.Teams
                .AnyAsync(
                    t => t.Name == team.Name
                    &&
                    t.OrganizationId == team.OrganizationId
                );
        }

        public async Task<Team> GetById(string teamId)
        {
            return await _context.Teams
                .Include(t => t.Organization)
                .Include(t => t.Users).ThenInclude(tu => tu.User)
                .Include(t => t.Tickets)
                .FirstOrDefaultAsync(t => t.Id == teamId);
        }

        public async Task<IEnumerable<Team>> GetByOrganizationId(string organizationId)
        {
            return await _context.Teams
                .Include(t => t.Organization)
                .Include(t => t.Users).ThenInclude(tu => tu.User)
                .Include(t => t.Tickets)
                .Where(t => t.OrganizationId == organizationId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetByUser(string userId)
        {
            var teamUsers = await _context.TeamUsers
                .Include(tu => tu.Team)
                .Where(tu => tu.User.Id == userId).ToListAsync();
            var teams = new List<Team>();
            foreach (var team in teamUsers)
            {
                teams.Add(team.Team);
            }
            return teams;
        }

        public Task<IEnumerable<User>> GetMembers(string teamId)
        {
            throw new NotImplementedException();
        }

        public Task<Team> Modify(Team team)
        {
            throw new NotImplementedException();
        }
    }
}
