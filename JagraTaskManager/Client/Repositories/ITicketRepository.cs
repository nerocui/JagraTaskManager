using JagraTaskManager.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JagraTaskManager.Client.Repositories
{
    public interface ITicketRepository
    {
        Task<List<TicketForListDto>> GetTicketsByCreator();
        Task<List<TicketForListDto>> GetTicketsByAssignee();
        Task<List<TicketForListDto>> GetTicketsByWatcher();
        Task<List<TicketForListDto>> GetTicketsByTeam(string teamId);
        Task<List<TicketForListDto>> GetTicketsByOrganization(string orgId);
        Task<TicketForListDto> CreateTicket(TicketForCreationDto ticket);
        Task<TicketForListDto> GetTicket(string ticketId);
        Task<TicketForListDto> UpdateTicketTitle(TicketForListDto ticket);
        Task<TicketForListDto> UpdateTicketDescription(TicketForListDto ticket);
        Task<TicketForListDto> UpdateTicketAssignee(TicketForListDto ticket);
    }
}