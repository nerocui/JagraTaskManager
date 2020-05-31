using JagraTaskManager.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JagraTaskManager.Server.Data
{
    public interface IInvitationRepository
    {
        Task<Invitation> Add(Invitation invitation);
        bool Delete(Invitation invitation);
        Task<bool> InvitationExist(string organizationId, string userId);
        Task<IEnumerable<Invitation>> GetInvitationsByOrganization(string organizationId);
        Task<IEnumerable<Invitation>> GetInvitationsByUser(string userId);
    }
}
