using JagraTaskManager.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Data
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly ApplicationDbContext _context;

        public InvitationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Invitation> Add(Invitation invitation)
        {
            await _context.Invitations.AddAsync(invitation);
            await _context.SaveChangesAsync();
            return await _context
                .Invitations
                .Include(i => i.User)
                .Include(i => i.Organization)
                .FirstOrDefaultAsync(i => i.OrganizationId == invitation.OrganizationId && i.UserId == invitation.UserId);
        }

        public bool Delete(Invitation invitation)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invitation>> GetInvitationsByOrganization(string organizationId)
        {
            return await _context.Invitations
                .Include(i => i.User)
                .Include(i => i.Organization)
                .Where(i => i.OrganizationId == organizationId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Invitation>> GetInvitationsByUser(string userId)
        {
            return await _context.Invitations
                .Include(i => i.User)
                .Include(i => i.Organization)
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> InvitationExist(string organizationId, string userId)
        {
            return await _context
                .Invitations
                .AnyAsync(i => i.OrganizationId == organizationId && i.UserId == userId);
        }
    }
}
