using JagraTaskManager.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Data
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _context;
        public OrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Organization> AddUser(string organizationId, string userId, string role)
        {
            OrganizationUser ou = new OrganizationUser();
            ou.Role = role;
            ou.OrganizationId = organizationId;
            ou.UserId = userId;
            await _context.OrganizationUsers.AddAsync(ou);
            await _context.SaveChangesAsync();
            return await GetOrganization(organizationId);
        }

        public async Task<Organization> Create(Organization organization, User user)
        {
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
            var org = await _context.Organizations.FirstOrDefaultAsync(o => o.Name == organization.Name);
            return await AddUser(org.Id, user.Id, "Admin");
        }

        public async Task<bool> DeleteOrganization(Organization organization)
        {
            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> RemoveUser(Organization organization, User user)
        {
            var ou = await _context.OrganizationUsers.FirstOrDefaultAsync(ou => ou.UserId == user.Id && ou.OrganizationId == organization.Id);
            _context.OrganizationUsers.Remove(ou);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> OrganizationExistByName(string name)
        {
            if (await _context.Organizations.AnyAsync(x => x.Name == name))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> OrganizationExistById(string id)
        {
            if (await _context.Organizations.AnyAsync(x => x.Id == id))
            {
                return true;
            }
            return false;
        }

        public async Task<Organization> GetOrganization(string Id)
        {
            var organization = await _context.Organizations
                .Include(o => o.Users)
                .Include(o => o.Invitations).ThenInclude(i => i.User)
                .Include(o => o.Tickets)
                .Include(o => o.Teams)
                .FirstOrDefaultAsync(o => o.Id == Id);
            return organization;
        }

        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            var organizations = await _context.Organizations
                .Include(o => o.Users)
                .Include(o => o.Invitations).ThenInclude(i => i.User)
                .Include(o => o.Tickets)
                .Include(o => o.Teams)
                .ToListAsync();
            return organizations;
        }

        public async Task<IEnumerable<Organization>> GetOrganizationsByUser(string userId)
        {
            var organizationUsers = await _context
                .OrganizationUsers
                .Include(ou => ou.Organization)
                    .ThenInclude(o => o.Users)
                .Where(ou => ou.UserId == userId)
                .ToListAsync();
            var organizations = new List<Organization>();
            foreach (var ou in organizationUsers)
            {
                organizations.Add(ou.Organization);
            }
            return organizations;
        }

        public async Task<bool> IsAdmin(Organization organization, User user)
        {
            var relationship = await _context.OrganizationUsers.FirstOrDefaultAsync(r => r.OrganizationId == organization.Id && r.UserId == user.Id);
            if (relationship.Role == "Admin")
            {
                return true;
            }
            return false;
        }

        public async Task<Organization> UpdateOrganization(Organization org)
        {
            _context.Organizations.Update(org);
            await _context.SaveChangesAsync();
            return org;
        }

        public async Task<IEnumerable<User>> GetUsersByOrganization(Organization organization)
        {
            var organizationUsers = await _context
                .OrganizationUsers
                .Where(ou => ou.OrganizationId == organization.Id)
                .Include(ou => ou.User)
                .ToListAsync();
            List<User> users = new List<User>();
            foreach (var ou in organizationUsers)
            {
                users.Add(ou.User);
            }
            return users;
        }

        public async Task<bool> UserInOrganization(string userId, string organizationId)
        {
            return await _context.OrganizationUsers
                .AnyAsync(
                    ou => ou.UserId == userId
                    && 
                    ou.OrganizationId == organizationId
                );
        }
    }
}
