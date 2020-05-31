using JagraTaskManager.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUser(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetUsersByOrganization(string organizationId)
        {
            var orgUsers = await _context.OrganizationUsers
                .Include(ou => ou.User)
                .Where(ou => ou.OrganizationId == organizationId)
                .ToListAsync();
            var users = new List<User>();
            foreach (var ou in orgUsers)
            {
                users.Add(ou.User);
            }
            return users;
        }

        public async Task<bool> UserExist(string id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> UserExistByEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UserExistByUserName(string userName)
        {
            return await _context.Users.AnyAsync(u => u.UserName == userName);
        }
    }
}
