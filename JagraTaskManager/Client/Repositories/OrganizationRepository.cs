using JagraTaskManager.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JagraTaskManager.Client.Services;

namespace JagraTaskManager.Client.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly string _rootUrl = "api/organization";
        private readonly IHttpService _httpService;

        public OrganizationRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<OrganizationForListDto> CreateOrganization(OrganizationForCreationDto organization)
        {
            var response = await _httpService.Post<OrganizationForCreationDto, OrganizationForListDto>($"{_rootUrl}/create", organization);

            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new Exception($"Failed to Create Organization. {response.HttpResponseMessage}");
            }
        }

        public async Task<List<OrganizationForListDto>> GetOrganizations()
        {
            var response = await _httpService.Get<List<OrganizationForListDto>>($"{_rootUrl}/byuser");
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new Exception($"Failed to get list of organizations. {response.HttpResponseMessage}");
            }
        }

        public async Task<OrganizationForListDto> GetOrganization(string Id)
        {
            var response = await _httpService.Get<OrganizationForListDto>($"{_rootUrl}?organizationId={Id}");
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new Exception($"Failed to get Organization. {response.HttpResponseMessage}");
            }
        }
    }
}
