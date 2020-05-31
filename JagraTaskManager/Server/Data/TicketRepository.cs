using JagraTaskManager.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Data
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Ticket> Create(Ticket ticket)
        {
            var tracker = await _context.Tickets.AddAsync(ticket);
            await _context.TicketWatches.AddAsync(new TicketWatch { TicketId = tracker.Entity.Id, UserId = ticket.CreatorId });
            await _context.SaveChangesAsync();
            return tracker.Entity;
        }

        public Task<Ticket> Delete(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public async Task<Ticket> GetById(string ticketId)
        {
            return await GetAllMembers()
                .FirstOrDefaultAsync(t => t.Id == ticketId);
        }

        public async Task<IEnumerable<Ticket>> GetByOrganization(string orgId)
        {
            return await GetAllMembers()
                .Where(t => t.OrganizationId == orgId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetByTeamId(string teamId)
        {
            return await GetAllMembers()
                .Where(t => t.TeamId == teamId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Ticket>> GetByAssignee(string userId)
        {
            return await GetAllMembers()
                .Where(t => t.AssigneeId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetByCreator(string userId)
        {
            return await GetAllMembers()
                .Where(t => t.CreatorId == userId)
                .ToListAsync();
        }

        private IIncludableQueryable<Ticket, ICollection<TicketDependency>> GetAllMembers()
        {
            return _context.Tickets
                .Include(t => t.Creator)
                .Include(t => t.Assignee)
                .Include(t => t.Organization)
                .Include(t => t.Team)
                .Include(t => t.Watchers).ThenInclude(w => w.User)
                .Include(t => t.Status).ThenInclude(s => s.Status)
                .Include(t => t.Tags).ThenInclude(t => t.Tag)
                .Include(t => t.Dependers).ThenInclude(d => d.Dependee).ThenInclude(d => d.Dependers)
                .Include(t => t.Dependees).ThenInclude(d => d.Dependee).ThenInclude(d => d.Dependers);
        }

        public async Task<IEnumerable<Ticket>> GetByWatcher(string userId)
        {
            return await GetAllMembers()
                .Where(t => t.Watchers
                    .Select(w => w.UserId == userId)
                    .Any())
                .ToListAsync();
        }

        public async Task<Ticket> Modify(Ticket ticket)
        {
            var entity = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticket.Id);
            entity = ticket;
            _context.Tickets.Update(entity);
            await _context.SaveChangesAsync();
            return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticket.Id);
        }

        public async Task<bool> ExistById(string Id)
        {
            return await _context.Tickets.AnyAsync(t => t.Id == Id);
        }
    }
}
