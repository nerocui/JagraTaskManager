using System.Collections.Generic;
using System.Threading.Tasks;
using JagraTaskManager.Shared.Dto;

namespace JagraTaskManager.Client.Repositories
{
    public interface IUserRepository
    {
         Task<ICollection<UserForListDto>> GetUsersByOrganization(string orgId);
         Task<UserForListDto> SearchByEmail(string email);
    }
}