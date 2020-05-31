using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JagraTaskManager.Client.Services;
using JagraTaskManager.Shared.Dto;

namespace JagraTaskManager.Client.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly string _rootUrl = "api/invitation";
        private readonly IHttpService _httpService;
        public InvitationRepository(IHttpService httpService)
        {
            _httpService = httpService;

        }
        public async Task<List<InvitationForListDto>> CreateInvitation(InvitationForCreationDto invitation)
        {
            var response = await _httpService.Post<InvitationForCreationDto, List<InvitationForListDto>>($"{_rootUrl}/create", invitation);
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new Exception($"Fail to create invitation, {response.HttpResponseMessage}");
            }
        }

        public async Task<List<InvitationForListDto>> GetInvitationByOrg(string organizationId)
        {
            var response = await _httpService.Get<List<InvitationForListDto>>($"{_rootUrl}/byOrg?organizationId={organizationId}");
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new Exception($"Failed to get invitations by organization ID {organizationId}, {response.HttpResponseMessage}");
            }
        }

        public async Task<List<InvitationForListDto>> GetInvitationByUser()
        {
            var response = await _httpService.Get<List<InvitationForListDto>>($"{_rootUrl}/byUser");
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new Exception($"Failed to get invitations , {response.HttpResponseMessage}");
            }
        }
    }
}