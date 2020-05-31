using JagraTaskManager.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Data
{
    public interface IOrganizationRepository
    {
        /**
        * Creates an organization with the given organization model, user is admin
        */
        Task<Organization> Create(Organization organization, User user);
        /**
        * After the user click join, they will be added to the organization by this function
        */
        Task<Organization> AddUser(string organizationId, string userId, string role);
        Task<User> RemoveUser(Organization organization, User user);
        Task<bool> DeleteOrganization(Organization organization);
        Task<bool> OrganizationExistByName(string name);
        Task<bool> OrganizationExistById(string id);
        Task<bool> IsAdmin(Organization organization, User user);
        Task<Organization> GetOrganization(string Id);
        Task<IEnumerable<Organization>> GetOrganizationsByUser(string id);
        Task<IEnumerable<Organization>> GetOrganizations();
        Task<Organization> UpdateOrganization(Organization org);
        Task<IEnumerable<User>> GetUsersByOrganization(Organization organization);
        Task<bool> UserInOrganization(string userId, string organizationId);
    }
}
