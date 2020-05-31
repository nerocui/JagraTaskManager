using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JagraTaskManager.Client.Services;
using JagraTaskManager.Shared.Dto;

namespace JagraTaskManager.Client.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _rootUrl = "api/user";
        private readonly IHttpService _httpService;

        public UserRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<ICollection<UserForListDto>> GetUsersByOrganization(string orgId)
        {
            var response = await _httpService.Get<List<UserForListDto>>($"{_rootUrl}/byorg?orgId={orgId}");
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new Exception($"Failed to get Users. {response.HttpResponseMessage}");
            }
        }

        public async Task<UserForListDto> SearchByEmail(string email)
        {
            var response = await _httpService.Get<UserForListDto>($"{_rootUrl}/searchByEmail?email={email}");
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new Exception($"Fail to search for user with email {email}, {response.HttpResponseMessage}");
            }
        }
    }
}