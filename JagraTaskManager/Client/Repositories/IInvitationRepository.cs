using System.Collections.Generic;
using System.Threading.Tasks;
using JagraTaskManager.Shared.Dto;

namespace JagraTaskManager.Client.Repositories
{
    public interface IInvitationRepository
    {
        Task<List<InvitationForListDto>> CreateInvitation(InvitationForCreationDto invitation);
        Task<List<InvitationForListDto>> GetInvitationByOrg(string organizationId);
        Task<List<InvitationForListDto>> GetInvitationByUser();
    }
}