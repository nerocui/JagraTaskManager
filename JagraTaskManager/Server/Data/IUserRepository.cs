using JagraTaskManager.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Data
{
    public interface IUserRepository
    {
        Task<User> GetUser(string id);
        Task<User> GetUserByUserName(string userName);
        Task<User> GetUserByEmail(string email);
        Task<bool> UserExist(string id);
        Task<bool> UserExistByUserName(string userName);
        Task<bool> UserExistByEmail(string email);
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> GetUsersByOrganization(string organizationId);
    }
}
