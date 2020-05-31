using JagraTaskManager.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Data
{
    public interface ITicketRepository
    {
        Task<Ticket> Create(Ticket ticket);
        Task<Ticket> GetById(string ticketId);
        Task<Ticket> Modify(Ticket ticket);
        Task<IEnumerable<Ticket>> GetByTeamId(string teamId);
        Task<IEnumerable<Ticket>> GetByOrganization(string orgId);
        Task<IEnumerable<Ticket>> GetByCreator(string userId);
        Task<IEnumerable<Ticket>> GetByAssignee(string userId);
        Task<IEnumerable<Ticket>> GetByWatcher(string userId);
        Task<bool> ExistById(string Id);
        Task<Ticket> Delete(Ticket ticket);
    }
}
