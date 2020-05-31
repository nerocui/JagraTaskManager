using JagraTaskManager.Shared.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JagraTaskManager.Client.Repositories
{
    public interface IOrganizationRepository
    {
        Task<List<OrganizationForListDto>> GetOrganizations();
        Task<OrganizationForListDto> CreateOrganization(OrganizationForCreationDto organization);
        Task<OrganizationForListDto> GetOrganization(string Id);
    }
}
